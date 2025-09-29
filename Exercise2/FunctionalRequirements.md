# Functional Requirements

## Assumptions

- Aircraft registration number is retrieved once at flight creation and does not change.
- Aircraft registration REST API is internal, and fronted by Apigee/Akamai (OAuth2).
- Messages for a flight may not be partitioned together in Event Hub; design must handle out-of-order and duplicate messages.
- Store last processed message GUID and timestamp for deduping and ordering.
- Event volume: ~100k messages/day, 7k flights/day, flight event arrives up to 72hrs before departure.
- Flight Lookup API is read-only, exposed via Apigee/Akamai.
- Required availability is 99.99%(Critical), with disaster recovery.
- After ARRIVE, flight data is marked for archival.
- No PII or sensitive data; encryption is not required.
- Azure is the main platform; Kpaas/AKS maybe an internal option.
- Dev team is .NET-based; ops/platform support depends on chosen option.

## System Functional Flow

1. Event Hub Consumer reads a message from Event Hub.
2. Consumer parses the message and determines its type (FLIGHT, ETD, ETA, GATE, DEPART, ARRIVE).
3. If message type is FLIGHT:
    - Check if flight exists in Flight Domain Store.
    - If not, call Aircraft Registration REST API to get registration number.
    - Create new flight domain object with all details and registration number.
    - Save flight domain object to Flight Domain Store.
4. If message type is an update (ETD, ETA, GATE, DEPART, ARRIVE):
    - Load existing flight domain object from Flight Domain Store.
    - Compare message GUID/timestamp to last processed for duplicate message and ordering.
    - If message is newer and not duplicate, update relevant fields.
    - To prevent concurrent updates, apply locking or optimistic concurrency control:
           - Use either locking or optimistic concurrency to prevent conflicting updates and ensure data integrity.
    - Save updated flight domain object to Flight Domain Store.
5. Flight Lookup API is called by clients to retrieve flight data.
6. After ARRIVE message, mark flight for archival (retention policy).

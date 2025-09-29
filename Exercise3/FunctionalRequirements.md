# Functional Requirements – Hotel Voucher Notification System

## Assumptions
- Only hotel voucher issued notifications are supported, as required by the business use case.
- Agents have access to front-line applications at the gate, but building a front-end is not in scope for this solution; our requirement is only to provide a notification solution to send hotel vouchers via SMS text or push notification to the AA app.
- We do not expect duplicate cancellation events for the same flight; therefore, no data store is required for deduplication or event validation.
- There is no Reinstate event in this use case; only cancellation events are processed.

## Scope
Design a process to automatically notify checked-in passengers of cancelled flights (maintenance/crew only) via the push to AA app or SMS, issuing hotel vouchers if no more nonstop flights are available to their destination that day.

- Process is triggered by the flight cancellation event (from Event Hub).
- Only maintenance/crew cancellations are eligible; weather cancellations are excluded.
- Flight Status service is used to confirm cancellation and reason.
- Flight Availability service is used to check for remaining nonstop flights on the same day.
- If there are more nonstop flights to the destination, do not trigger this process. Most of the time this process will happen later at night if the last flight is cancelled.
- Only checked-in passengers are eligible; Passengers checked-in service is used (not Passengers booked service).
- NotificationHub only requires reservation code and notification type (HOTEL_VOUCHER_ISSUED) to send SMS/app notification; contact lookup is handled internally by NotificationHub.

Monitoring, alerting, and audit logging will be implemented using ELM, Moogsoft, and xMatters.
High availability (HA), disaster recovery (DR), and scaling requirements will be addressed in the design phase.


## Functional Flow
1. Receive flight cancellation event from Flight Status Event Hub.
2. Confirm cancellation and reason (maintenance/crew) via Flight Status service.
3. Check for remaining nonstop flights using Flight Availability service.
4. If there are more nonstop flights to the destination, do not trigger this process. (Most of the time this process will happen later at night if the last flight is cancelled.)
5. If no more nonstop flights are available, retrieve checked-in passengers via Passengers checked-in service and extract all unique reservation codes.
6. For each unique reservation code, send HOTEL_VOUCHER_ISSUED notification via NotificationHub using the reservation code and notification type.
7. NotificationHub manages delivery via SMS text/push to app.

## Notification Type Supported
- HOTEL_VOUCHER_ISSUED: Hotel voucher notification sent to checked-in passenger.

## External Services
- Flight Status Event Hub
- Flight Status service
- Flight Availability service
- Passengers checked-in service
- NotificationHub


---
# High-Level Design – Hotel Voucher Notification System

## Overview


## Logical Components (Conceptual)
All components below are implemented as internal modules/classes within a single .NET application deployed to one Azure App Service:
- **Event Consumer**: Subscribes to Flight Status Event Hub for cancellation events.
- **Orchestrator**: Coordinates the workflow—validates cancellation reason, checks flight availability, retrieves checked-in passengers, and extracts unique reservation codes.
- **Notification Service**: Sends hotel voucher notifications to NotificationHub using reservation codes.
- **External Services**:
  - Flight Status Event Hub
  - Flight Status Service
  - Flight Availability Service
  - Passengers Checked-In Service
  - NotificationHub


## Security & Networking
- **Azure Key Vault**: Secure storage for secrets, connection strings, and credentials.
- **Virtual Network (VNet)**: All components deployed within AA's Azure VNet; App Service and supporting resources reside in a dedicated subnet.
- **Palo Alto Firewall Layer**: Enterprise-managed perimeter security for ingress/egress traffic (shown in overall network architecture, not app-specific).
- **Akamai/Apigee Gateway**: All outbound calls to external/internal APIs are routed through Akamai and Apigee gateways, as per enterprise standards.

## Monitoring & Logging
- **Application Insights**: Telemetry, logging, and performance monitoring for the .NET app.
- **Dynatrace**: Enterprise monitoring and observability platform for deep application and infrastructure insights.
- **ELM**: App logs flow to Nifi, then to ADX and Grafana for analytics, with ADX forwarding to Data Lake for enterprise warehousing.
- **Moogsoft, xMatters**: Integrated for system-wide monitoring and alerting.

## Scalability, HA, DR
- **App Service Scaling**: Configured for autoscale based on event volume.
- **High Availability**: Multi-instance deployment within Azure region.
- **Disaster Recovery**: Geo-redundant backup and failover for App Service and Key Vault. No database is used, so DR is limited to application and secrets.

## Sequence of Operations
1. Flight Status Event Hub publishes a cancellation event.
2. Event Consumer receives the event and invokes Orchestrator.
3. Orchestrator validates cancellation reason (maintenance/crew) via Flight Status Service.
4. Orchestrator checks for remaining nonstop flights via Flight Availability Service.
5. If no more nonstop flights, Orchestrator retrieves checked-in passengers and extracts unique reservation codes.
6. Notification Service sends hotel voucher notification to NotificationHub for each reservation code.
7. NotificationHub delivers SMS/push notification to passengers.


- **GitHub Actions**: Automated build, test, and deployment pipeline for the .NET application.
- **SonarQube**: Continuous code quality and test coverage analysis.
- **Nucleus Vulnerability Scan**: Enterprise-wide vulnerability scanning for all repositories and Azure resources, integrated via resource tags.
- **Coverity Scan**: Static code analysis for security and quality (may be replaced by Tenable bots reporting to Nucleus vulnerability management).

## Diagrams
See separate files for:
  - Conceptual Diagram
  - Sequence Diagram
  - Deployment & Networking Diagram

---

# High-Level Design (HLD) – Flight Event Processing System

## Scope and Objectives
- Process flight event messages (~100k/day) and provide flight lookup API.
- Support .NET team, Azure platform.

## Major Components
- Azure Event Hub (ingestion)
- .NET Core App (App Service or Kpaas AKS)
- Managed SQL/Mongo DB
- REST API for flight lookup
- CI/CD pipeline (Azure DevOps, GitHub Actions, or Kpaas-provided)

## Data Flow
- Event Hub receives flight events.
- .NET App processes events, enriches with Aircraft Registration API.
- Stores/updates flight data in DB.
- API exposes flight data to clients.

## Integration Points
- Aircraft Registration REST API (internal)
- Apigee/Akamai for API exposure/security

## Security, HA, and DR
- RBAC, Key Vault, VNet (App Service) or Network Policies (Kpaas)
- .NET Core App and REST API: Deployed to Azure App Service with autoscale enabled (minimum 2 instances for HA).
- Managed DB: Configured with geo-replication and multiple replicas/replica sets for HA and DR. For SQL DB, use active geo-replication; for Mongo DB, use replica sets.
- Event Hub: Multi-region support and platform-managed HA.
- All components monitored for health and failover readiness.

## Deployment Overview
- Option 2: Deploy .NET App to Azure App Service, CI/CD via Azure DevOps/GitHub Actions.
- Option 4: Deploy .NET App to Kpaas AKS, CI/CD managed by platform team.

## Key Design Decisions
- Chose .NET Core for team fit and extensibility.
- App Service for simplicity, Kpaas for internal platform alignment.
- Managed DB for reliability and lower ops overhead.

## Conclusion

Based on the requirements for high availability, scalability, and team expertise, the recommended solution is to implement a .NET Core application hosted on Azure App Service. This approach leverages Azure Event Hub for message ingestion, integrates with the Aircraft Registration REST API, and uses a managed database for persistence. The solution provides robust scaling, built-in HA, and aligns with the team's .NET skills. Monitoring and logging are integrated via Application Insights, Dynatrace, and ELM. This architecture is maintainable, extensible, and meets all specified requirements.
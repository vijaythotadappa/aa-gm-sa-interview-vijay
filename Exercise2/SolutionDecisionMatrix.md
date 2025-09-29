
# Solution Decision Matrix

---

## Option 1: Stream Analytics + Functions

**Pros:**
- Rapid delivery for simple flows
- Managed scaling and HA
- Low maintenance overhead

**Cons:**
- Limited for complex business logic
- Cache enrichment is not real-time, also have to build chachig mechanism 
- Less control over transformation logic

**Cost Estimate:** Medium (Event Hub, Stream Analytics, Functions, SQL/Mongo DB, Networking)
**Skills Needed:** Azure, Stream Analytics
**HA/Scalability:** High (serverless, autoscale)
**Maintenance Overhead:** Low (managed services)
**Security:** Azure RBAC, VNet, Key Vault
**DR/Geo Redundancy:** Multi-region supported
**CI/CD:** Azure DevOps, GitHub Actions
**Notes:** Good for simple flows, limited logic

**Trade-off:**
Best for rapid delivery and simple transformation, but not suitable for complex enrichment or business logic. Aircraft registration enrichment relies on joining to a cache, and real-time cache updates are not straightforward.

---

## Option 2: .NET Core App (Azure Apps)

**Pros:**
- Flexible for complex business logic
- Direct REST API enrichment
- Good fit for .NET team

**Cons:**
- Moderate maintenance overhead
- Slightly higher operational cost than serverless

**Cost Estimate:** Medium (Event Hub, App Service, SQL/Mongo DB, Networking)
**Skills Needed:** .NET Core, Azure SDK
**HA/Scalability:** High (App Service autoscale)
**Maintenance Overhead:** Moderate
**Security:** RBAC, Key Vault, VNet
**DR/Geo Redundancy:** Geo-replication available
**CI/CD:** Azure DevOps, GitHub Actions
**Notes:** Team is .NET based, flexible

**Trade-off:**
Best for complex business logic and integration, and supports direct REST API enrichment for aircraft registration. Good fit for .NET teams and long-term improvements.

---

## Option 3: Hybrid (Analytics + .NET)

**Pros:**
- Scalable for very high throughput
- Separation of concerns
- Can optimize cost by splitting workloads

**Cons:**
- Increased complexity and integration risk
- Higher maintenance overhead
- Requires expertise in both Azure Stream analytics and .NET

**Cost Estimate:** Medium-High (Event Hub, Stream Analytics, App Service, SQL/Mongo DB, Networking)
**Skills Needed:** Azure, .NET Core
**HA/Scalability:** High (split scaling)
**Maintenance Overhead:** Moderate-High
**Security:** RBAC, Key Vault, VNet
**DR/Geo Redundancy:** Multi-region possible
**CI/CD:** Azure DevOps, GitHub Actions
**Notes:** Best for large scale, more complexity

**Trade-off:**
Hybrid is designed for very high throughput and separation of concerns, but for 100k messages/day (moderate scale), simpler options may suffice. Use Hybrid only if future scale is expected to grow significantly; otherwise, it adds unnecessary complexity and integration risk.

---

## Option 4: Kpaas Kubernetes Platform


**Pros:**
- Maximum flexibility and control
- Can run any language/runtime
- Custom scaling and networking
- Can onboard 

**Cons:**
- Requires Kubernetes knowledge for troubleshooting
- Potential for onboarding and maintainance issues if team is not familiar with Kpaas

**Cost Estimate:** High (Event Hub, AKS, SQL/Mongo DB, Networking)
**Skills Needed:** Kubernetes, DevOps, Azure
**HA/Scalability:** High (customizable)
**Maintenance Overhead:** Medium-High
**Security:** RBAC, Network Policies, Key Vault
**DR/Geo Redundancy:** Multi-region possible
**CI/CD:** Provided and managed by Kpaas team
**Notes:** Use only if reduce maintainance overhead

**Trade-off:**
Functionally the same as Option 2 (.NET Core App), but deployed in a Kpaas-provisioned cluster. CI/CD and security are handled by the Kpaas team;  main responsibility is hosting the database (managed or self-hosted) and deploying app to the provided cluster.

---

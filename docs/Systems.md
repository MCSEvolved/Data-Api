# Systems

## Models

<details>
<summary>System</summary>

```csharp
class System
{
    int id;
    string displayName;
    string? description;
    string[]? produces; //item names in 'minecraft:item_name' syntax

}
```

</details>


---
## REST Endpoints

<details>
<summary>Add New System</summary>

Add a new system

| Name | Value |
| --- | --- |
| URL | `api.mcsynergy.nl/mcs-api/system/new` |
| Method | `POST` |
| Body | ` System as JSON ` |
| Headers | `Authorization` |
| Required Role | `isService` |
| Success Response | Code: 200|
| Error Response | Code: 400 <br> Content: `JSON is Invalid` |
| Error Response | Code: 400 <br> Content: `System already exists` |
| Error Response | Code: 401/403 <br> Content: `Not Authenticated/Authorized` |


</details>

---

<details>
<summary>Update System</summary>

Update a system

| Name | Value |
| --- | --- |
| URL | `api.mcsynergy.nl/mcs-api/system/update` |
| Method | `PUT` |
| Body | ` System as JSON ` |
| Headers | `Authorization` |
| Required Role | `isService` |
| Success Response | Code: 200|
| Error Response | Code: 400 <br> Content: `JSON is Invalid` |
| Error Response | Code: 400 <br> Content: `System doesn't exists` |
| Error Response | Code: 401/403 <br> Content: `Not Authenticated/Authorized` |


</details>

---

<details>
<summary>Delete System</summary>

Delete a system

| Name | Value |
| --- | --- |
| URL | `api.mcsynergy.nl/mcs-api/system/delete` |
| Method | `DELETE` |
| URL Params | `id: int` |
| Headers | `Authorization` |
| Required Role | `isService` |
| Success Response | Code: 200|
| Error Response | Code: 400 <br> Content: `System doesn't exists` |
| Error Response | Code: 401/403 <br> Content: `Not Authenticated/Authorized` |


</details>

---

<details>
<summary>Get All Systems</summary>

Get a list of all the systems in the database

| Name | Value |
| --- | --- |
| URL | `api.mcsynergy.nl/mcs-api/system/get/all` |
| Method | `GET` |
| Headers | `Authorization` |
| Required Claim | `isGuest` |
| Success Response | Code: 200 <br> Content: `List<System> as JSON` |
| Error Response | Code: 401 <br> Content: `Not Authorized` |
| Error Response | Code: 404 <br> Content: `No systems Found` |

</details>

---

<details>
<summary>Get System by Name</summary>

Get a systen by its name

| Name | Value |
| --- | --- |
| URL | `api.mcsynergy.nl/mcs-api/system/get/by-name` |
| Method | `GET` |
| URL Params | `name: string` |
| Headers | `Authorization` |
| Required Claim | `isGuest` |
| Success Response | Code: 200 <br> Content: `System, as JSON` |
| Error Response | Code: 401 <br> Content: `Not Authorized` |
| Error Response | Code: 404 <br> Content: `System by Name: [name] not found` |

</details>

---

<details>
<summary>Get System by ID</summary>

Get a systen by its ID

| Name | Value |
| --- | --- |
| URL | `api.mcsynergy.nl/mcs-api/system/get/by-id` |
| Method | `GET` |
| URL Params | `id: int` |
| Headers | `Authorization` |
| Required Claim | `isGuest` |
| Success Response | Code: 200 <br> Content: `System, as JSON` |
| Error Response | Code: 401 <br> Content: `Not Authorized` |
| Error Response | Code: 404 <br> Content: `System by ID: [id] not found` |

</details>

# Services

## Models

<details>
<summary>Service</summary>

```csharp
class Service
{
    string id;
    string name;
    string displayName;
    Image[]? images; //only fill this in when updating a service, see the image model for an explanation why

}
```

</details>

<details>
<summary>Image</summary>

**!! This image object is filled automatically when you upload an image so you only need to pass this when updating a service otherwise it will be overwritten !!**

```csharp
class Image
{
    string name; /*something like 'favicon_1' or 'banner_large', 
                    make sure that it is clear what the image is used for */
    string path; /*url of the image, most likely 
                    'https://api.mcsynergy.nl/mcs-api/images/services/[service-id]/[image].[extension]' */

}
```

</details>


---
## REST Endpoints

<details>
<summary>Add New Service</summary>

Add a new service

| Name | Value |
| --- | --- |
| URL | `api.mcsynergy.nl/mcs-api/service/new` |
| Method | `POST` |
| Body | ` Service as JSON ` |
| Headers | `Authorization` |
| Required Role | `isService` |
| Success Response | Code: 200|
| Error Response | Code: 400 <br> Content: `JSON is Invalid` |
| Error Response | Code: 400 <br> Content: `Service already exists` |
| Error Response | Code: 401/403 <br> Content: `Not Authenticated/Authorized` |


</details>

---

<details>
<summary>Add New Image</summary>

Add a new service

| Name | Value |
| --- | --- |
| URL | `api.mcsynergy.nl/mcs-api/service/new/image` |
| Method | `POST` |
| Body | ` key: image, value: uploaded file as Form-Data ` |
| URL Params | `serviceId: string` |
| URL Params | `imageName: string` |
| Headers | `Authorization` |
| Required Role | `isService` |
| Success Response | Code: 200|
| Error Response | Code: 400 <br> Content: `Service doesn't exists` |
| Error Response | Code: 401/403 <br> Content: `Not Authenticated/Authorized` |


</details>

---

<details>
<summary>Update service</summary>

Its a good practise to first GET the service and then change it, otherwise you might forget keys and they will then be removed from the DB.  
Update a service

| Name | Value |
| --- | --- |
| URL | `api.mcsynergy.nl/mcs-api/service/update` |
| Method | `PUT` |
| Body | ` Service as JSON ` |
| Headers | `Authorization` |
| Required Role | `isService` |
| Success Response | Code: 200|
| Error Response | Code: 400 <br> Content: `JSON is Invalid` |
| Error Response | Code: 400 <br> Content: `Service doesn't exists` |
| Error Response | Code: 401/403 <br> Content: `Not Authenticated/Authorized` |


</details>

---

<details>
<summary>Delete Service</summary>

Delete a service

| Name | Value |
| --- | --- |
| URL | `api.mcsynergy.nl/mcs-api/service/delete` |
| Method | `DELETE` |
| URL Params | `id: string` |
| Headers | `Authorization` |
| Required Role | `isService` |
| Success Response | Code: 200|
| Error Response | Code: 400 <br> Content: `Service doesn't exists` |
| Error Response | Code: 401/403 <br> Content: `Not Authenticated/Authorized` |


</details>

---

<details>
<summary>Get All Services</summary>

Get a list of all the services in the database

| Name | Value |
| --- | --- |
| URL | `api.mcsynergy.nl/mcs-api/service/get/all` |
| Method | `GET` |
| Headers | `Authorization` |
| Required Claim | `isGuest` |
| Success Response | Code: 200 <br> Content: `List<Service> as JSON` |
| Error Response | Code: 401 <br> Content: `Not Authorized` |
| Error Response | Code: 404 <br> Content: `No services Found` |

</details>

---

<details>
<summary>Get Service by Name</summary>

Get a Service by its name

| Name | Value |
| --- | --- |
| URL | `api.mcsynergy.nl/mcs-api/service/get/by-name` |
| Method | `GET` |
| URL Params | `name: string` |
| Headers | `Authorization` |
| Required Claim | `isGuest` |
| Success Response | Code: 200 <br> Content: `service, as JSON` |
| Error Response | Code: 401 <br> Content: `Not Authorized` |
| Error Response | Code: 404 <br> Content: `service by Name: [name] not found` |

</details>

---

<details>
<summary>Get Service by ID</summary>

Get a Service by its ID

| Name | Value |
| --- | --- |
| URL | `api.mcsynergy.nl/mcs-api/service/get/by-id` |
| Method | `GET` |
| URL Params | `id: string` |
| Headers | `Authorization` |
| Required Claim | `isGuest` |
| Success Response | Code: 200 <br> Content: `service, as JSON` |
| Error Response | Code: 401 <br> Content: `Not Authorized` |
| Error Response | Code: 404 <br> Content: `service by ID: [id] not found` |

</details>

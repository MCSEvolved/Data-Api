# Items

## Models

<details>
<summary>Item</summary>

```csharp
class Item
{
    string name;
    string displayName;
    int stackSize;
    bool smeltable
    Image? image; //only fill this in when updating a item, see the image model for an explanation why

}
```

</details>

<details>
<summary>Image</summary>

**!! This image object is filled automatically when you upload an image so you only need to pass this when updating a item otherwise it will be overwritten !!**

It is better that every item image is the same size, it should probably be a square image so something like 200x200 or 400x400

```csharp
class Image
{
    string name; /*is the same as Item.name */
    string path; /*url of the image, most likely 
                    'https://api.mcsynergy.nl/mcs-api/images/services/[item-id]/[image].[extension]' */

}
```

</details>


---
## REST Endpoints

<details>
<summary>Add New Item</summary>

Add a new item

| Name | Value |
| --- | --- |
| URL | `api.mcsynergy.nl/mcs-api/item/new` |
| Method | `POST` |
| Body | ` Item as JSON ` |
| Headers | `Authorization` |
| Required Role | `isService` |
| Success Response | Code: 200|
| Error Response | Code: 400 <br> Content: `JSON is Invalid` |
| Error Response | Code: 400 <br> Content: `Item already exists` |
| Error Response | Code: 401/403 <br> Content: `Not Authenticated/Authorized` |


</details>

---

<details>
<summary>Add New Image</summary>

Add a new item

| Name | Value |
| --- | --- |
| URL | `api.mcsynergy.nl/mcs-api/item/new/image` |
| Method | `POST` |
| Body | ` key: image, value: uploaded file as Form-Data ` |
| URL Params | `itemName: string` |
| Headers | `Authorization` |
| Required Role | `isService` |
| Success Response | Code: 200|
| Error Response | Code: 400 <br> Content: `Item doesn't exists` |
| Error Response | Code: 401/403 <br> Content: `Not Authenticated/Authorized` |


</details>

---

<details>
<summary>Update item</summary>

Its a good practise to first GET the item and then change it, otherwise you might forget keys and they will then be removed from the DB.  
Update a item

| Name | Value |
| --- | --- |
| URL | `api.mcsynergy.nl/mcs-api/item/update` |
| Method | `PUT` |
| Body | ` Item as JSON ` |
| Headers | `Authorization` |
| Required Role | `isService` |
| Success Response | Code: 200|
| Error Response | Code: 400 <br> Content: `JSON is Invalid` |
| Error Response | Code: 400 <br> Content: `Item doesn't exists` |
| Error Response | Code: 401/403 <br> Content: `Not Authenticated/Authorized` |


</details>

---

<details>
<summary>Delete item</summary>

Delete a item

| Name | Value |
| --- | --- |
| URL | `api.mcsynergy.nl/mcs-api/item/delete` |
| Method | `DELETE` |
| URL Params | `name: string` |
| Headers | `Authorization` |
| Required Role | `isService` |
| Success Response | Code: 200|
| Error Response | Code: 400 <br> Content: `Item doesn't exists` |
| Error Response | Code: 401/403 <br> Content: `Not Authenticated/Authorized` |


</details>

---

<details>
<summary>Get Items by Names</summary>

Get multiple items with a single call

| Name | Value |
| --- | --- |
| URL | `api.mcsynergy.nl/mcs-api/item/get/by-names` |
| Method | `GET` |
| Headers | `Authorization` |
| Required Claim | `isGuest` |
| URL Params | `names: string[]` |
| Success Response | Code: 200 <br> Content: `List<Item> as JSON` |
| Error Response | Code: 401 <br> Content: `Not Authorized` |
| Error Response | Code: 404 <br> Content: `No items Found` |

</details>

---

<details>
<summary>Get All Smeltable Items</summary>

Get a list of all the services in the database

| Name | Value |
| --- | --- |
| URL | `api.mcsynergy.nl/mcs-api/item/get/smeltable` |
| Method | `GET` |
| Headers | `Authorization` |
| Required Claim | `isGuest` |
| Success Response | Code: 200 <br> Content: `List<Item> as JSON` |
| Error Response | Code: 401 <br> Content: `Not Authorized` |
| Error Response | Code: 404 <br> Content: `No items Found` |

</details>

---

<details>
<summary>Get item by Name</summary>

Get a item by its name

| Name | Value |
| --- | --- |
| URL | `api.mcsynergy.nl/mcs-api/item/get/by-name` |
| Method | `GET` |
| URL Params | `name: string` |
| Headers | `Authorization` |
| Required Claim | `isGuest` |
| Success Response | Code: 200 <br> Content: `Item as JSON` |
| Error Response | Code: 401 <br> Content: `Not Authorized` |
| Error Response | Code: 404 <br> Content: `Item by Name: [name] not found` |

</details>

---

<details>
<summary>Check if item is smeltable</summary>


| Name | Value |
| --- | --- |
| URL | `api.mcsynergy.nl/mcs-api/item/is-smeltable` |
| Method | `GET` |
| URL Params | `name: string` |
| Headers | `Authorization` |
| Required Claim | `isGuest` |
| Success Response | Code: 200 <br> Content: `boolean` |
| Error Response | Code: 401 <br> Content: `Not Authorized` |
| Error Response | Code: 404 <br> Content: `Item by Name: [name] not found` |

</details>

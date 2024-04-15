
# **Catio**

Welcome to my ASP.NET MVC Application for a subscription based cat food shop.

---

## **Summary:**

Catio is a web application for purchasing cat food, suppliments, toys and accessories. Which can be bought as a subscripton or a one time purchase.

---

## **DataBase:**

For the DataBase I've used MS SQL Server.

![alt text](https://i.ibb.co/7g2bGzs/image.png)

###
* **Customers** (AspNetUsers) - Collection of all of the registered users of the application.
* **Products** - Collection of all the products.
* **Categories** - Collection of categories for the Products.
* **Addresses** - Collection of all the addresses.
* **Orders** - Collecttion of all the orders.
* **ProductsOrders** - Mapping table between Products and Orders.
* **Statuses** - Collection of statuses for the Orders.
* **SubscriptionBoxes** - Collection of SubscriptionBoxes.
* **ProductSubscriptionBoxes** - Mapping table between Poducts and SubscriptionBoxes

---

## **Functionality:**

### **Roles:**
The types of roles are:
* Admin
    * **Admin Username: admin@gmail.com**
    * **Admin Password: admin123456789**
* User
    * Every newly created account will be set as a "User".
---
### **User functionality:**

![alt text](https://i.ibb.co/N3W0Xvk/image.png)

###
* Clicking on the **"Catio"** logo redirects the customer to the main page.
* Clicking on the **"Products"** button the customer is redirected to the products page.
    * In the products page the customer has access to all of the products with additional button for **"Details"**

        * <img src = "https://i.ibb.co/N38wZsY/image.png" width="175" height="250" ></M>
    * Additionally in the "Details" page there is the "Add to Cart" button which adds the product to the cart.
        * ![alt text](https://i.ibb.co/kx5mXXx/image.png)
* Clicking on the "Subscriptions" logo redirects the user to the main page.
* Clicking on the "Catio" logo redirects the user to the main page.
* Clicking on the "Catio" logo redirects the user to the main page.

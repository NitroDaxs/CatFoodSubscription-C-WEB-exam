
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
* Clicking on the **"Subscriptions"** logo redirects the user to the subscriptions page.
    * All orders with subscription based products will be shown to the customer.
        * A button to cancel will be available under every order.
            * ![alt text](https://i.ibb.co/PFHznpP/image.png)
* Clicking on the **"About Us"** logo redirects the user to about us page.
    * There is more information and google Api map to show the RL location.
* Clicking on the **"Cart"** logo redirects the user to cart page.
    * All products and subscrion box will be shown in the cart.
        * When the customer is ready to make his order there is a chechout button.
            * <img src = "https://i.ibb.co/jWY8wBT/image.png" width="" height="30" >
            * After clicking on **"Checkout"** the customer is redirected to the order finilizing and address form.
                * <img src = "https://i.ibb.co/JdYFVgy/image.png" width="" height="40">
* Typing in the **Search box** user will recieve all products that match the name query.
* Hovering over the **Profile** icon the already logged in customer will have **Orders** button.
    * <img src = "https://i.ibb.co/KF5h7XF/image.png" width="" height="100">

        * Upon clicking on the "Order" button the customer will be shown all of his orders with a "View Order" button.

        * <img src = "https://i.ibb.co/FHVD3DY/image.png" width="" height="30">

            * After clicking the button the customer will see all information of his order.

---

### **Admin functionality:**

* When the Admin is logged in in the navigation bar there will be an aditional button.**"Admin Panel"**
    * <img src = "https://i.ibb.co/qMx3gn0/image.png" width="" height="30" >

        * Upon clicking on the button the Admin will be redirected to the admin page.
         * <img src="https://i.ibb.co/X36jGLd/image.png" alt="image" width="" height="260">
        
        * After clicking on the **"All Products"** button the admin is redirected to the page.
            * <img src="https://i.ibb.co/FHRQg78/image.png" alt="image" width="" height="">

                * Upon clicking on the **"Edit"** button the admin will be redirected to a edit form for the product.
                * Upon clicking on the **"Delete"** button the admin will be asked if he is sure to delete this product. (Soft delete)
                    * The **"Delete"** button becomes **"Restore"** so in the future the product can be brought back.
                        * <img src="https://i.ibb.co/8KhQdhr/image.png" alt="image" width="" height="35">
        * Upon clicking on the **"Add Product"** button the Admin will be redirected to an form for the new product.
            * <img src="https://i.ibb.co/Cw47w5M/image.png" alt="image" width="" height="300">
        * Upon clicking on the **All Orders"** button the Admin will be redirected to the page.
            * <img src="https://i.ibb.co/860dqTq/image.png" alt="image" width="" height="">

                * Button "View Info" will show the Admin all of the information about the specified order.
                * Button "Change Status" allows the Admin to change the order's status.

                    * <img src="https://i.ibb.co/SwVr4nh/image.png" alt="image" width="" height="120">
        * Upon clicking **"All Subscriptions"** the Admin will be shown a page with all of the subscription orders and will be able to renew the orders
            
<details>
  <summary>Main Page</summary>
  <img src="https://i.ibb.co/1fk050S/image.png" alt="image" width="" height="">
</details>
<details>
  <summary>Products Page</summary>
  <img src="https://i.ibb.co/L9zcbR5/image.png" alt="image" width="" height="">
</details>
<details>
  <summary>Details Page</summary>
  <img src="https://i.ibb.co/72b6Dr7/image.png" alt="image" width="" height="">
</details>
<details>
  <summary>Subscriptions Page</summary>
  <img src="https://i.ibb.co/XVc0fSj/image.png" alt="image" width="" height="">
</details>
<details>
  <summary>About Us Page</summary>
  <img src="https://i.ibb.co/nwwGqh9/image.png" alt="image" width="" height="">
</details>
<details>
  <summary>Cart Page</summary>
  <img src="https://i.ibb.co/G38NZ5H/image.png" alt="image" width="" height="">
</details>
<details>
  <summary>Checkout Page</summary>
  <img src="https://i.ibb.co/nCkDgB7/image.png" alt="image" width="" height="">
</details>
<details>
  <summary>Orders Page</summary>
  <img src="https://i.ibb.co/NpygGJH/image.png" alt="image" width="" height="">
</details>
<details>
  <summary>Admin Panel Page</summary>
  <img src="https://i.ibb.co/TqTjJyg/image.png" alt="image" width="" height="">
</details>
<details>
  <summary>All Products Page</summary>
  <img src="https://i.ibb.co/xS9sbsW/image.png" alt="image" width="" height="">
</details>
<details>
  <summary>Add Product Page</summary>
  <img src="https://i.ibb.co/NZ3pCYd/image.png" alt="image" width="" height="">
</details>
<details>
  <summary>All Orders Page</summary>
  <img src="https://i.ibb.co/tx21hzP/image.png" alt="image" width="" height="">
</details>
<details>
  <summary>All Subscriptions Page</summary>
  <img src="https://i.ibb.co/Tqz2GcN/image.png" alt="image" width="" height="">
</details>

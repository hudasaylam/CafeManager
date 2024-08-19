# CafeManager
Streamlined self-service café management system for 'Roast and Relax,' built with C# Windows Forms and MSSQL. Features include automated order calculations, dynamic product and discount management, and comprehensive admin control
System Features and Mechanism
Automatic Total Calculation:

The system automatically calculates the total amount for each order, taking into account the prices of selected products and applying any available discounts based on the current date.
Admin Management:

Administrators have the ability to manage the system by adding or deleting products, discounts, and other admin users. This provides flexibility and control over the café's offerings and promotions.
Self-Service Focus:

Designed specifically for a self-service café, the system does not include table management features. This simplifies the ordering process and aligns with the café's operational model.
Database Structure:

Products Table: Stores details of products, including variations in size such as small, medium, large, and standard.
Orders Table: Displays orders, tracking each one placed by customers.
OrderList Table: Manages all product order operations, functioning like a cart to keep track of items selected by customers.
Discounts Table: Handles discounts, which can be either percentage-based or fixed amounts. Discounts are applicable to specific dates and products and can be managed by the admin.
Admins Table: Contains essential data required for admin login and authentication, ensuring secure access to the system.
this is an example for adding order operation :
![example](https://github.com/user-attachments/assets/47603800-b5f7-45ea-ade8-1ad6fb960732)
A new product operation 
![example2](https://github.com/user-attachments/assets/2099f8b8-4130-4a0c-b0ab-a0e1ffc90f59)


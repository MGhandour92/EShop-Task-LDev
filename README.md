<div id="top"></div>

<!--
*** I'm using markdown "reference style" links for readability.
*** Reference links are enclosed in brackets [ ] instead of parentheses ( ).
*** See the bottom of this document for the declaration of the reference variables
*** for contributors-url, forks-url, etc. This is an optional, concise syntax you may use.
*** https://www.markdownguide.org/basic-syntax/#reference-style-links
-->

<br />
<div align="center">
  <h1 align="center">Electronics Shop Sample Project</h3>
  <h3 align="center">A project is a sample for an Electronics online shop for selling multiple electronic products</h3>
</div>


<!-- TABLE OF CONTENTS -->
<details>
  <summary>**Table of Contents**</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#database">Database</a></li>
        <li><a href="#system-flow">System Flow</a></li>
      </ul>
    </li>
	<li>
      <a href="#contact">Contact</a>
	 </li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

![Eshop Home Page][product-screenshot]


The project is simulating the experience of online shopping, an Electronics online shop with multiple products from different categories.
You Can:
1. Fill your cart with products
2. Place an order to your address, even if you are not registered
3. You will have discounts on buying even number of products

<p align="right">(<a href="#top">back to top</a>)</p>



### Built With

* [DotNet Core 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
* [SQL Server Express LocalDB](https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb?view=sql-server-ver16)

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- GETTING STARTED -->
## Getting Started

You may check the word document (*Requirements.docx*) for more information

### Database

![DB Diagram][db-diagram]

We have the Category table that contains 3 main categories (**TVs, Laptops and Sound Systems**). The products are categorized by a one-to-many relationship between Category and Product tables.

The Product table has the information and price for each product, also the discount that will be applied (***if the customer bought doubles of the same product***).

And finally we have the Order header and lines tables, that refelcts the order placed by the customer with summing the quantities and total price for the order.

### System Flow

1. Adminstrators can manage categories and products only

![Admin UseCases][admin-usecases]

2. Registered and un-registered Customers can start shopping, adding products to the cart and then place the order for shipping

![Customer UseCases][customers-usecases]

3. Discounts applied when adding the products to the cart.

![Process Flow][flow-process]

4. Discount Formula >> **Amount to be cut from the total = (Item Qty X Item Unit Price X Discount %)**

<p align="right">(<a href="#top">back to top</a>)</p>

<!-- CONTACT -->
## Contact

Mohamed El Ghandour - m.el_ghandour@hotmail.com 

Project Link: (https://github.com/MGhandour92/EShop-Task-LDev)

<p align="right">(<a href="#top">back to top</a>)</p>

<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[product-screenshot]: README_images/ProductScreen.png
[db-diagram]: README_images/DB_Diagram.png
[admin-usecases]: README_images/Admin_UseCases.png
[customers-usecases]: README_images/Customers_UseCases.png
[flow-process]: README_images/Flow_Chart.png
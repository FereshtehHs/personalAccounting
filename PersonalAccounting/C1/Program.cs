// See https://aka.ms/new-console-template for more information
using Accounting.DataLayer.Repository;
using Accounting.DataLayer.Services;

ICustomerRepository customer = new CustomerRepository();
var list = customer.GetAllCustomers();
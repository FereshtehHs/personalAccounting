﻿using Accounting.DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Accounting.ViewModel.Customers;

namespace Accounting.DataLayer.Services
{
    public class CustomerRepository : ICustomerRepository
    {
        PersonalAccounting_DBEntities db;
        public CustomerRepository(PersonalAccounting_DBEntities context)
        {
            db = context;
        }

        public bool DeleteCustomer(Customers customer)
        {
            try
            {
                db.Entry(customer).State = EntityState.Deleted;
                return true;
            }
            catch
            { return false; }
        }

        public bool DeleteCustomerById(int customerId)
        {

            try
            {
                var customer = GetCustomerById(customerId);
                DeleteCustomer(customer);
                return true;
            }
            catch
            { return false; }
        }

        public List<Customers> GetAllCustomers()
        {
            return db.Customers.ToList();
        }

        public Customers GetCustomerById(int customerId)
        {
            return db.Customers.Find(customerId);
        }

        public List<ListCustomerViewModel> GetCustomerNames(string filter = "")
        {
            if (filter == "")
            {
                return db.Customers.Select(c => new ListCustomerViewModel()
                {
                    CustomerID = c.CustomerID,
                    FulName = c.FulName
                }).ToList();
            }
            return db.Customers.Where(c => c.FulName.Contains(filter))
                .Select(c => new ListCustomerViewModel()
                {
                    CustomerID = c.CustomerID,
                    FulName = c.FulName
                }).ToList();
    }

    public IEnumerable<Customers> GetCustomersByFilter(string parameter)
    {
        return db.Customers.Where(c => c.FulName.Contains(parameter) || c.Email.Contains(parameter) || c.Mobile.Contains(parameter)).ToList();
    }

    public bool InsertCustomer(Customers customer)
    {
        try
        {
            db.Customers.Add(customer);
            return true;
        }
        catch
        { return false; }
    }

    public bool UpdateCustomer(Customers customer)
    {
        //  try
        //{
        var local = db.Set<Customers>()
                     .Local
                     .FirstOrDefault(f => f.CustomerID == customer.CustomerID);
        if (local != null)
        {
            db.Entry(local).State = EntityState.Detached;
        }
        db.Entry(customer).State = EntityState.Modified;
        return true;
        //}
        //catch 
        // { return false; }
    }
}
}

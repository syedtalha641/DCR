﻿using DAL.EntityModels;
using DCR.Helper.ViewModel;
using Microsoft.EntityFrameworkCore;
using Repository.IRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repos
{
    public class BranchRepos : IBranchrepos
    {
        private readonly EMS_ITCContext _context;

        public BranchRepos(EMS_ITCContext context)
        {
            _context = context;
        }
        public async Task<BranchViewModel> AddBranch(BranchViewModel model)
        {
            try
            {
                // Create a new User entity
                var newBranch = new Branch
                {
                    BranchName = model.Name,
                    BranchPerson = model.Person,
                    BranchEmail = model.Email,
                    BranchPhone = model.Phone,
                    BranchAddress = model.Address,
                    Country = model.Country,
                    BranchCity = model.City,
                    BranchState = model.State,
                    BranchPostalCode = model.PostalCode,

                };
                newBranch.CreatedBy = "Admin";

                _context.Branches.Add(newBranch);
                await _context.SaveChangesAsync();


                // Convert the Customer entity to CustomerViewModel
                var BranchViewModel = new BranchViewModel
                {
                    Name = newBranch.BranchName,
                    Person = newBranch.BranchPerson,
                    Email = newBranch.BranchEmail,
                    Phone = newBranch.BranchPhone,
                    Address = newBranch.BranchAddress,
                    Country = newBranch.Country,
                    City = newBranch.BranchCity,
                    State = newBranch.BranchState,
                    PostalCode = newBranch.BranchPostalCode
                };



                return BranchViewModel;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<Branch> DeleteBranch(int BranchId)
        {

            var result = await _context.Branches.Where(a => a.BranchId == BranchId).FirstOrDefaultAsync();
            if (result != null)
            {
                result.IsActive = false;
                await _context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<Branch> GetBranch(int BranchId)
        {
            return await _context.Branches.FirstOrDefaultAsync(a => a.BranchId == BranchId);
        }

        public async Task<IEnumerable<Branch>> GetBranches()
        {
            return await _context.Branches.ToListAsync();
        }

        public async Task<BranchViewModel> UpdateBranch(int BranchId, BranchViewModel model)
        {
            var result = await _context.Branches.FirstOrDefaultAsync(a => a.BranchId == BranchId);
            if (result != null)
            {

                result.BranchName = model.Name;
                result.BranchPerson = model.Person;
                result.BranchEmail = model.Email;
                result.BranchPhone = model.Phone;
                result.BranchAddress = model.Address;
                result.Country = model.Country;
                result.BranchCity = model.City;
                result.BranchState = model.State;
                result.BranchPostalCode = model.PostalCode;


                // Update other fields if needed, e.g., UpdatedBy and UpdatedOn
                //result.UpdatedBy = model.CustomerName; // Set the appropriate value
                result.UpdatedOn = DateTime.Now; // Set the appropriate value
                                                 // Save changes to the database
                await _context.SaveChangesAsync();


                return model;


            }
            else
            {
                // User not found
                return null;
            }
        }
    }

    }

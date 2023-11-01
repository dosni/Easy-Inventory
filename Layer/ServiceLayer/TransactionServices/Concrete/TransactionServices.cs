using Common;
using DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Model;
using DataLayer.EntityStock;
using productTransaction = DataLayer.EntityStock.Transaction;
using ServiceLayer.ProductTransactionServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.TransactionServices.Concrete
{
    public class TransactionServices
    {
        private readonly StockContext _context;
        private readonly TransactionLineServices _lineServices;
        public TransactionServices(StockContext context, TransactionLineServices lineServices)
        {
            _context = context;
            _lineServices = lineServices;
        }

        /// <summary>
        /// Membuat Custom AutoNumber.
        /// Menambah satu kepada setiap record baru
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetIDAsync()
        {

            try
            {
                var ocat = await _context.Transactions
                   .OrderBy(pc => pc.TransactionId)
                   .LastOrDefaultAsync();

                if (ocat != null)
                {
                    return (ocat.TransactionId + 1);
                }
                else
                {
                    return 1;

                }

            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        private async Task<ServiceResponseDTO<bool>> AddAsync(TransactionDto objDTO)
        {
            ServiceResponseDTO<bool> result = new();
           
            try
            {
                var ItemToAdd = new productTransaction
                {
                    TransactionId = objDTO.TransactionId,
                    TransactionDate = objDTO.TransactionDate ?? DateTime.Now, // Converting from non Nullable to nullable . if null assgin to datetime.Now
                    CreatedAt = objDTO.CreatedAt,
                    ContactId = objDTO.ContactId,
                    CreatedBy = objDTO.CreatedBy,
                    Description = objDTO.Description,
                    IsPosted = true
                };
                _context.Add(ItemToAdd);
                var affectedRows = await _context.SaveChangesAsync();
                result.Success = affectedRows > 0;
                result.Message = result.Success ? "Data saved" : "Data is not saved";
                return result;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Success = false;
                return result;
            }
        }
    

        public async Task<ServiceResponseDTO<bool>> CreateAsync(TransactionDto objDTO,IEnumerable<TransactionLineDto> linesDto)
        {
            ServiceResponseDTO<bool> result = new();
            int Id = await GetIDAsync();
            if (Id == -1)
            {
                result.Message = "Tidak bisa simpan data";
                result.Success = false;
                return result;
            }
            objDTO.TransactionId = Id;
            result=await AddAsync(objDTO);

            int type =(int) Convert.ToInt32(objDTO.TransactionType);
          
            switch (type)
            {
                case (int)TransactionType.InitialStock:
                    result =  await _lineServices.AddInitialStockAsync(Id, linesDto);
                    break;
                case (int) TransactionType.Purchase:
                    result = await _lineServices.AddPurchaseAsync(Id, linesDto);
                    break;
                case (int)TransactionType.Purchase_Return:
                    // code block
                    break;
                case (int)TransactionType.Sales:
                    // code block
                    break;
                case (int)TransactionType.Sales_Return:
                    // code block
                    break;
                case (int)TransactionType.Adjustment:
                    // code block
                    break;
                default:
                    // code block
                    break;
            }

           return result;
        }

    }
}

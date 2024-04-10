using AutoMapper;

using PFM.Domain.Entities;

namespace PFM.WebApp.Dtos;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<BillCreateDto, Bill>();
        CreateMap<BillUpdateDto, Bill>();
        
        CreateMap<BudgetCreateDto, Budget>();
        CreateMap<BudgetUpdateDto, Budget>();
        
        CreateMap<CategoryCreateDto, Category>();
        CreateMap<CategoryUpdateDto, Category>();

        CreateMap<DebtCreateDto, Debt>();
        CreateMap<DebtUpdateDto, Debt>();
        
        CreateMap<TransactionCreateDto, Transaction>();
        CreateMap<TransactionUpdateDto, Transaction>();
    }
}
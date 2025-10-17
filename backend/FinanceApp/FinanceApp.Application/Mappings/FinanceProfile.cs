using AutoMapper;
using FinanceApp.Application.DTOs;
using FinanceApp.Domain.Entities;
using FinanceApp.Domain.ListItem;
using FinanceApp.Domain.Queries;

namespace FinanceApp.Application.Mappings
{
    public class FinanceProfile : Profile
    {
        public FinanceProfile()
        {
            CreateMap<BankEntity, BankDTO>();
            CreateMap<CreateBankDTO, BankEntity>();
            CreateMap<UpdateBankDTO, BankEntity>();

            CreateMap<CurrencyEntity, CurrencyDTO>();
            CreateMap<CreateCurrencyDTO, CurrencyEntity>();
            CreateMap<UpdateCurrencyDTO, CurrencyEntity>();

            CreateMap<DepositCertificateEntity, DepositCertificateDTO>();
            CreateMap<CreateDepositCertificateDTO, DepositCertificateEntity>();
            CreateMap<UpdateDepositCertificateDTO, DepositCertificateEntity>();
            CreateMap<DepositCertificateReadOnlyView, DepositCertificateReadOnlyViewDTO>();

            CreateMap<CardEntity, CardDTO>();
            CreateMap<UpdateCardDTO, CardEntity>();
            CreateMap<CardReadOnlyView, CardReadOnlyViewDTO>();

            CreateMap<ListItemEntity, ListItemDTO>();
        }
    }
}

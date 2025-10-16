using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Reservoom.Services;
using Reservoom.Stores;
using Reservoom.ViewModels;

namespace Reservoom.HostBuilders;

public static class AddViewModelHostBuilderExtensions
{
    public static IHostBuilder AddViewModel(this IHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureServices((context, services) =>
        {
            services.AddSingleton((s) => CreateReservationListingViewModel(s));
            services.AddSingleton<Func<ReservationListingViewModel>>(s => () => s.GetRequiredService<ReservationListingViewModel>());
            services.AddSingleton<NavigationService<ReservationListingViewModel>>();

            services.AddSingleton<MakeReservationViewModel>();
            services.AddSingleton<Func<MakeReservationViewModel>>(s => () => s.GetRequiredService<MakeReservationViewModel>());
            services.AddSingleton<NavigationService<MakeReservationViewModel>>();

            services.AddSingleton<MainViewModel>();
        });

        return hostBuilder;
    }

    private static ReservationListingViewModel CreateReservationListingViewModel(IServiceProvider s)
    {
        return ReservationListingViewModel.LoadViewModel(
            s.GetRequiredService<HotelStore>(),
            s.GetRequiredService<NavigationService<MakeReservationViewModel>>());
    }
}

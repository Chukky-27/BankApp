using Core;
using MFCore.MFServices.MFImplementations;
using MFCore.MFServices.MfInterfaces;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddScoped<IAuth, Auth>();
services.AddSingleton<MF_HomePage>();
services.AddScoped<ITransactions, Transactions>();

var serviceProvider = services.BuildServiceProvider();
var home = serviceProvider.GetRequiredService<MF_HomePage>();

home.Run();
# Migration
Add-Migration InitialModel -context Infrastructure.Data.ApplicationDbContext -o Data/Migrations
update-database -context Infrastructure.Data.ApplicationDbContext

Add-Migration InitialIdentityModel -context Infrastructure.Identity.AppIdentityDbContext -o Identity/Migrations
update-database -context Infrastructure.Identity.AppIdentityDbContext


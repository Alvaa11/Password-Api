using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.UsersModel;
using Requests;


namespace Routes.UsersRoutes
{
    public static class UsersRoutes
    {
        public static void UsersRoute(this WebApplication app)
        {
            var route = app.MapGroup("/api/v01/users");

            route.MapGet("", 
            async (UsersContext context) =>
            {
                var users = await context.Users.ToListAsync();
                return Results.Ok(users);
                
            });  


            route.MapPost("/", 
            async (UsersContext context, UsersRequest req) => 
            {
                var user = new UsersModel(req.Username, req.Password);
                if(user.VerifyUser(req.Username) == true)
                {
                    return Results.BadRequest("User already exists");
                }
                else
                {
                    await context.Users.AddAsync(user);
                    await context.SaveChangesAsync();
                    return Results.Ok(user);
                }

            });

            route.MapPut("/{id:int}", 
            async (int Id, UsersContext context, UsersRequest req) => 
            {
                var user = await context.Users.FirstOrDefaultAsync(u => u.Id == Id);;
                if(user == null || user.VerifyUser(req.Username) == true)
                {
                    return Results.BadRequest("User already exists or not found");
                }
                else {
                    user.SetUsername(req.Username, req.Password);
                    await context.SaveChangesAsync();
                    return Results.Ok(user);
                }
            });

            route.MapDelete("/{id:int}", 
            async (int Id, [FromServices] UsersContext context) => 
            {
                var user = await context.Users.FirstOrDefaultAsync(u => u.Id == Id);;
                if(user == null)
                {
                    return Results.NotFound("User not found");
                }
                else {
                    context.Users.Remove(user);
                    await context.SaveChangesAsync();
                    return Results.Ok(user);
                }
            });
        }
        
    }
}
   
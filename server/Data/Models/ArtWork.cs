using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using CuratedArt.Data;
namespace CuratedArt.Data.Models
{
    public class ArtWork
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Desc { get; set; }

        public DateTimeOffset DateReleased { get; set; }

        public ArtWorkType Type { get; set; }
    }

    public enum ArtWorkType
    {
        Painting,
        Music,
        Movie
    }

    //public static class ArtWorkRoutes
    //{
    //    public static void MapArtWorkEndpoints(this IEndpointRouteBuilder routes)
    //    {
    //        routes.MapGet("/api/ArtWork", async (CuratedArtDbContext db) =>
    //        {
    //            return await db.ArtWorks.ToListAsync();
    //        })
    //        .WithName("GetAllArtWorks")
    //        .Produces<List<ArtWork>>(StatusCodes.Status200OK);

    //        routes.MapGet("/api/ArtWork/{id}", async (Guid Id, CuratedArtDbContext db) =>
    //        {
    //            return await db.ArtWorks.FindAsync(Id)
    //                is ArtWork model
    //                    ? Results.Ok(model)
    //                    : Results.NotFound();
    //        })
    //        .WithName("GetArtWorkById")
    //        .Produces<ArtWork>(StatusCodes.Status200OK)
    //        .Produces(StatusCodes.Status404NotFound);

    //        routes.MapPut("/api/ArtWork/{id}", async (Guid Id, ArtWork artWork, CuratedArtDbContext db) =>
    //        {
    //            var foundModel = await db.ArtWorks.FindAsync(Id);

    //            if (foundModel is null)
    //            {
    //                return Results.NotFound();
    //            }
    //            //update model properties here

    //            await db.SaveChangesAsync();

    //            return Results.NoContent();
    //        })
    //        .WithName("UpdateArtWork")
    //        .Produces(StatusCodes.Status404NotFound)
    //        .Produces(StatusCodes.Status204NoContent);

    //        routes.MapPost("/api/ArtWork/", async (ArtWork artWork, CuratedArtDbContext db) =>
    //        {
    //            db.ArtWorks.Add(artWork);
    //            await db.SaveChangesAsync();
    //            return Results.Created($"/ArtWorks/{artWork.Id}", artWork);
    //        })
    //        .WithName("CreateArtWork")
    //        .Produces<ArtWork>(StatusCodes.Status201Created);


    //        routes.MapDelete("/api/ArtWork/{id}", async (Guid Id, CuratedArtDbContext db) =>
    //        {
    //            if (await db.ArtWorks.FindAsync(Id) is ArtWork artWork)
    //            {
    //                db.ArtWorks.Remove(artWork);
    //                await db.SaveChangesAsync();
    //                return Results.Ok(artWork);
    //            }

    //            return Results.NotFound();
    //        })
    //        .WithName("DeleteArtWork")
    //        .Produces<ArtWork>(StatusCodes.Status200OK)
    //        .Produces(StatusCodes.Status404NotFound);
    //    }
    //}
}

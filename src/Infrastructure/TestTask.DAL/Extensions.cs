using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTask.Domain.Common;

namespace TestTask.DAL;

internal static class Extensions
{
	public static void ConfigureId<TEntity, TId>(this EntityTypeBuilder<TEntity> typeBuilder)
		where TEntity : Entity<TId>
		where TId : IValueId<TId>
	{
		typeBuilder.HasKey(e => e.Id);

		typeBuilder.Property(e => e.Id)
				.HasConversion(
					e => e.Value,
					e => (TId)Activator.CreateInstance(typeof(TId), e)!
				);
	}
}

using Microsoft.EntityFrameworkCore;

public class DeliveryContext : DbContext
{
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }

    public DeliveryContext(DbContextOptions<DeliveryContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.Property(c => c.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(c => c.Apellido)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(c => c.Direccion)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(c => c.Telefono)
                .IsRequired()
                .HasMaxLength(100);
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.Property(i => i.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(i => i.Descripción)
                .HasMaxLength(500);

            entity.Property(i => i.Precio)
                .IsRequired()
                .HasColumnType("decimal(18,2)"); 
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.Property(p => p.Fecha)
                .IsRequired();

            entity.HasOne(p => p.Cliente)
                .WithMany() // Asume que un cliente puede tener muchos pedidos
                .HasForeignKey("ClienteId") // Agrega la clave foránea en Pedido
                .OnDelete(DeleteBehavior.Cascade); // Comportamiento en la eliminación del cliente

            entity.HasMany(p => p.Items)
                .WithOne() // Relación uno a muchos con ítems
                .OnDelete(DeleteBehavior.Cascade); // Eliminar los ítems cuando se elimina el pedido

            entity.Property(p => p.EstadoPedido)
            .IsRequired();
        });
    }
}

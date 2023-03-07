using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DAL.Modelo;

public partial class CspharmaInformacionalContext : DbContext
{
    public CspharmaInformacionalContext()
    {
    }

    public CspharmaInformacionalContext(DbContextOptions<CspharmaInformacionalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DlkCatAccEmpleado> DlkCatAccEmpleados { get; set; }

    public virtual DbSet<DlkCatRole> DlkCatRoles { get; set; }

    public virtual DbSet<TdcCatEstadosDevolucionPedido> TdcCatEstadosDevolucionPedidos { get; set; }

    public virtual DbSet<TdcCatEstadosEnvioPedido> TdcCatEstadosEnvioPedidos { get; set; }

    public virtual DbSet<TdcCatEstadosPagoPedido> TdcCatEstadosPagoPedidos { get; set; }

    public virtual DbSet<TdcCatLineasDistribucion> TdcCatLineasDistribucions { get; set; }

    public virtual DbSet<TdcTchEstadoPedido> TdcTchEstadoPedidos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=cspharma_informacional;User Id=postgres;Password=doshermanas1");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DlkCatAccEmpleado>(entity =>
        {
            entity.HasKey(e => e.CodEmpleado).HasName("dlk_cat_acc_empleados_pkey");

            entity.ToTable("dlk_cat_acc_empleados", "dlk_informacional");

            entity.Property(e => e.CodEmpleado)
                .HasColumnType("character varying")
                .HasColumnName("cod_empleado");
            entity.Property(e => e.ClaveEmpleado)
                .HasColumnType("character varying")
                .HasColumnName("clave_empleado");
            entity.Property(e => e.MdDate)
                .HasColumnType("character varying")
                .HasColumnName("md_date");
            entity.Property(e => e.MdUuid)
                .HasColumnType("character varying")
                .HasColumnName("md_uuid");
            entity.Property(e => e.NivelAccesoEmpleado).HasColumnName("nivel_acceso_empleado");

            entity.HasOne(d => d.NivelAccesoEmpleadoNavigation).WithMany(p => p.DlkCatAccEmpleados)
                .HasForeignKey(d => d.NivelAccesoEmpleado)
                .HasConstraintName("fk_acceso");
        });

        modelBuilder.Entity<DlkCatRole>(entity =>
        {
            entity.HasKey(e => e.NivelAccesoEmpleado).HasName("dlk_cat_roles_pkey");

            entity.ToTable("dlk_cat_roles", "dlk_informacional");

            entity.Property(e => e.NivelAccesoEmpleado)
                .ValueGeneratedNever()
                .HasColumnName("nivel_acceso_empleado");
            entity.Property(e => e.Descripcion)
                .HasColumnType("character varying")
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<TdcCatEstadosDevolucionPedido>(entity =>
        {
            entity.HasKey(e => e.CodEstadoDevolucion).HasName("tdc_cat_estados_devolucion_pedido_pkey");

            entity.ToTable("tdc_cat_estados_devolucion_pedido", "dwh_torrecontrol");

            entity.Property(e => e.CodEstadoDevolucion)
                .HasComment("Código que identifica de forma unívoca el estado de devolución de un pedido.\n")
                .HasColumnType("character varying")
                .HasColumnName("cod_estado_devolucion");
            entity.Property(e => e.DesEstadoDevolucion)
                .HasComment("Descripción del estado de devolución del pedido.\n")
                .HasColumnType("character varying")
                .HasColumnName("des_estado_devolucion");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasComment("Identificador unívoco del estado de devolución del pedido en el sistema.\n")
                .HasColumnName("id ");
            entity.Property(e => e.MdDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("md_date");
            entity.Property(e => e.MdUuid)
                .HasComment("Código de metadato que indica el grupo de inserción al que pertenece el registro.\n")
                .HasColumnType("character varying")
                .HasColumnName("md_uuid");
        });

        modelBuilder.Entity<TdcCatEstadosEnvioPedido>(entity =>
        {
            entity.HasKey(e => e.CodEstadoEnvio).HasName("tdc_cat_estados_envio_pedido_pkey");

            entity.ToTable("tdc_cat_estados_envio_pedido", "dwh_torrecontrol");

            entity.Property(e => e.CodEstadoEnvio)
                .HasComment("Código que identifica de forma unívoca el estado de envío de un pedido.\n")
                .HasColumnType("character varying")
                .HasColumnName("cod_estado_envio");
            entity.Property(e => e.DesEstadoEnvio)
                .HasComment("Descripción del estado de envío del pedido.\n")
                .HasColumnType("character varying")
                .HasColumnName("des_estado_envio");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasComment("Identificador unívoco del estado de envío del pedido\nen el sistema.")
                .HasColumnName("id");
            entity.Property(e => e.MdDate)
                .HasComment("Fecha en la que se define le grupo de inserción.\n")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("md_date");
            entity.Property(e => e.MdUuid)
                .HasComment("Código de metadato que indica el grupo de inserción al que pertenece el registro.\n")
                .HasColumnType("character varying")
                .HasColumnName("md_uuid");
        });

        modelBuilder.Entity<TdcCatEstadosPagoPedido>(entity =>
        {
            entity.HasKey(e => e.CodEstadoPago).HasName("tdc_cat_estados_pago_pedido_pkey");

            entity.ToTable("tdc_cat_estados_pago_pedido", "dwh_torrecontrol");

            entity.Property(e => e.CodEstadoPago)
                .HasComment("Código que identifica de forma unívoca el estado de pago de un pedido.\n")
                .HasColumnType("character varying")
                .HasColumnName("cod_estado_pago");
            entity.Property(e => e.DesEstadoPago)
                .HasComment("Descripción del estado de pago del pedido.\n")
                .HasColumnType("character varying")
                .HasColumnName("des_estado_pago");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasComment("Identificador unívoco del estado de pago del pedido en el sistema.\n")
                .HasColumnName("id");
            entity.Property(e => e.MdDate)
                .HasComment("Fecha en la que se define el grupo de inserción.\n")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("md_date");
            entity.Property(e => e.MdUuid)
                .HasComment("Código de metadato que indica el grupo de inserción al que pertenece el registro.\n")
                .HasColumnType("character varying")
                .HasColumnName("md_uuid");
        });

        modelBuilder.Entity<TdcCatLineasDistribucion>(entity =>
        {
            entity.HasKey(e => e.CodLinea).HasName("tdc_cat_lineas_distribucion_pkey");

            entity.ToTable("tdc_cat_lineas_distribucion", "dwh_torrecontrol");

            entity.Property(e => e.CodLinea)
                .HasComment("Código que identifica de forma unívoca la línea de distribución por carretera que sigue el envío: codprovincia-codmunicipio-codbarrio.\n")
                .HasColumnType("character varying")
                .HasColumnName("cod_linea");
            entity.Property(e => e.CodBarrio)
                .HasComment("Código que identifica de forma unívoca el barrio.\n")
                .HasColumnType("character varying")
                .HasColumnName("cod_barrio");
            entity.Property(e => e.CodMunicipio)
                .HasComment("Código que identifica de forma unívoca el municipio.\n\n")
                .HasColumnType("character varying")
                .HasColumnName("cod_municipio");
            entity.Property(e => e.CodProvincia)
                .HasComment("Código que identifica de forma unívoca a la provincia.\n")
                .HasColumnType("character varying")
                .HasColumnName("cod_provincia");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasComment("Identificador unívoco de la línea de distribucíon en el sistema.\n")
                .HasColumnName("id");
            entity.Property(e => e.MdDate)
                .HasComment("Fecha en la que se define el grupo de inserción.\n")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("md_date");
            entity.Property(e => e.MdUuid)
                .HasComment("Código de de metadato que indica el grupo de inserción al que pertenece el registro.\n\n")
                .HasColumnType("character varying")
                .HasColumnName("md_uuid");
        });

        modelBuilder.Entity<TdcTchEstadoPedido>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tdc_tch_estado_pedidos_pkey");

            entity.ToTable("tdc_tch_estado_pedidos", "dwh_torrecontrol");

            entity.Property(e => e.Id)
                .HasComment("Identificador unívoco del pedido en el sistema.\n")
                .HasColumnName("id");
            entity.Property(e => e.CodEstadoDevolucion)
                .HasComment("Código que identifica de forma unívoca el estado de devolución de un pedido.\n")
                .HasColumnType("character varying")
                .HasColumnName("cod_estado_devolucion");
            entity.Property(e => e.CodEstadoEnvio)
                .HasComment("Código que identifica de forma unívoca el estado de envío de un pedido.\n")
                .HasColumnType("character varying")
                .HasColumnName("cod_estado_envio");
            entity.Property(e => e.CodEstadoPago)
                .HasComment("Código que identifica de forma unívoca el estado de pago de un pedido.\n")
                .HasColumnType("character varying")
                .HasColumnName("cod_estado_pago");
            entity.Property(e => e.CodLinea)
                .HasComment("Código que identifica de forma unívoca la línea de distribución por carretera que sigue el envío: codprovincia-codmunicipio-codbarrio.\n")
                .HasColumnType("character varying")
                .HasColumnName("cod_linea");
            entity.Property(e => e.CodPedido)
                .HasComment("Código que identifica de forma unívoca un pedido. Se construye con: provincia-codfarmacia-id.\n")
                .HasColumnType("character varying")
                .HasColumnName("cod_pedido");
            entity.Property(e => e.MdDate)
                .HasComment("Fecha en la que se define el grupo de inserción.\n")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("md_date");
            entity.Property(e => e.MdUuid)
                .HasComment("Código de metadato que indica el grupo de inserción al que pertenece el registro.")
                .HasColumnType("character varying")
                .HasColumnName("md_uuid");

            entity.HasOne(d => d.CodEstadoDevolucionNavigation).WithMany(p => p.TdcTchEstadoPedidos)
                .HasForeignKey(d => d.CodEstadoDevolucion)
                .HasConstraintName("cod_estado_devolucion_fk");

            entity.HasOne(d => d.CodEstadoEnvioNavigation).WithMany(p => p.TdcTchEstadoPedidos)
                .HasForeignKey(d => d.CodEstadoEnvio)
                .HasConstraintName("cod_estado_envio_fk");

            entity.HasOne(d => d.CodEstadoPagoNavigation).WithMany(p => p.TdcTchEstadoPedidos)
                .HasForeignKey(d => d.CodEstadoPago)
                .HasConstraintName("cod_estado_pago_fk");

            entity.HasOne(d => d.CodLineaNavigation).WithMany(p => p.TdcTchEstadoPedidos)
                .HasForeignKey(d => d.CodLinea)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cod_linea_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

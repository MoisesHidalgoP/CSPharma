using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DAL.Modelo
{
    public partial class cspharma_informacionalContext : DbContext
    {
        public cspharma_informacionalContext()
        {
        }

        public cspharma_informacionalContext(DbContextOptions<cspharma_informacionalContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DlkCatAccEmpleado> DlkCatAccEmpleados { get; set; } = null!;
        public virtual DbSet<TdcCatEstadosDevolucionPedido> TdcCatEstadosDevolucionPedidos { get; set; } = null!;
        public virtual DbSet<TdcCatEstadosEnvioPedido> TdcCatEstadosEnvioPedidos { get; set; } = null!;
        public virtual DbSet<TdcCatEstadosPagoPedido> TdcCatEstadosPagoPedidos { get; set; } = null!;
        public virtual DbSet<TdcCatLineasDistribucion> TdcCatLineasDistribucions { get; set; } = null!;
        public virtual DbSet<TdcTchEstadoPedido> TdcTchEstadoPedidos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=cspharma_informacional;User Id=postgres;Password=doshermanas1");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DlkCatAccEmpleado>(entity =>
            {
                entity.HasKey(e => e.CodEmpleado)
                    .HasName("dlk_cat_acc_empleados_pkey");

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

                entity.Property(e => e.NivelAccesoEmpleado)
                    .HasColumnType("character varying")
                    .HasColumnName("nivel_acceso_empleado");
            });

            modelBuilder.Entity<TdcCatEstadosDevolucionPedido>(entity =>
            {
                entity.HasKey(e => e.CodEstadoDevolucion)
                    .HasName("tdc_cat_estados_devolucion_pedido_pkey");

                entity.ToTable("tdc_cat_estados_devolucion_pedido", "dwh_torrecontrol");

                entity.Property(e => e.CodEstadoDevolucion)
                    .HasColumnType("character varying")
                    .HasColumnName("cod_estado_devolucion")
                    .HasComment("Código que identifica de forma unívoca el estado de devolución de un pedido.\n");

                entity.Property(e => e.DesEstadoDevolucion)
                    .HasColumnType("character varying")
                    .HasColumnName("des_estado_devolucion")
                    .HasComment("Descripción del estado de devolución del pedido.\n");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id ")
                    .HasComment("Identificador unívoco del estado de devolución del pedido en el sistema.\n");

                entity.Property(e => e.MdDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("md_date");

                entity.Property(e => e.MdUuid)
                    .HasColumnType("character varying")
                    .HasColumnName("md_uuid")
                    .HasComment("Código de metadato que indica el grupo de inserción al que pertenece el registro.\n");
            });

            modelBuilder.Entity<TdcCatEstadosEnvioPedido>(entity =>
            {
                entity.HasKey(e => e.CodEstadoEnvio)
                    .HasName("tdc_cat_estados_envio_pedido_pkey");

                entity.ToTable("tdc_cat_estados_envio_pedido", "dwh_torrecontrol");

                entity.Property(e => e.CodEstadoEnvio)
                    .HasColumnType("character varying")
                    .HasColumnName("cod_estado_envio")
                    .HasComment("Código que identifica de forma unívoca el estado de envío de un pedido.\n");

                entity.Property(e => e.DesEstadoEnvio)
                    .HasColumnType("character varying")
                    .HasColumnName("des_estado_envio")
                    .HasComment("Descripción del estado de envío del pedido.\n");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id")
                    .HasComment("Identificador unívoco del estado de envío del pedido\nen el sistema.");

                entity.Property(e => e.MdDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("md_date")
                    .HasComment("Fecha en la que se define le grupo de inserción.\n");

                entity.Property(e => e.MdUuid)
                    .HasColumnType("character varying")
                    .HasColumnName("md_uuid")
                    .HasComment("Código de metadato que indica el grupo de inserción al que pertenece el registro.\n");
            });

            modelBuilder.Entity<TdcCatEstadosPagoPedido>(entity =>
            {
                entity.HasKey(e => e.CodEstadoPago)
                    .HasName("tdc_cat_estados_pago_pedido_pkey");

                entity.ToTable("tdc_cat_estados_pago_pedido", "dwh_torrecontrol");

                entity.Property(e => e.CodEstadoPago)
                    .HasColumnType("character varying")
                    .HasColumnName("cod_estado_pago")
                    .HasComment("Código que identifica de forma unívoca el estado de pago de un pedido.\n");

                entity.Property(e => e.DesEstadoPago)
                    .HasColumnType("character varying")
                    .HasColumnName("des_estado_pago")
                    .HasComment("Descripción del estado de pago del pedido.\n");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id")
                    .HasComment("Identificador unívoco del estado de pago del pedido en el sistema.\n");

                entity.Property(e => e.MdDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("md_date")
                    .HasComment("Fecha en la que se define el grupo de inserción.\n");

                entity.Property(e => e.MdUuid)
                    .HasColumnType("character varying")
                    .HasColumnName("md_uuid")
                    .HasComment("Código de metadato que indica el grupo de inserción al que pertenece el registro.\n");
            });

            modelBuilder.Entity<TdcCatLineasDistribucion>(entity =>
            {
                entity.HasKey(e => e.CodLinea)
                    .HasName("tdc_cat_lineas_distribucion_pkey");

                entity.ToTable("tdc_cat_lineas_distribucion", "dwh_torrecontrol");

                entity.Property(e => e.CodLinea)
                    .HasColumnType("character varying")
                    .HasColumnName("cod_linea")
                    .HasComment("Código que identifica de forma unívoca la línea de distribución por carretera que sigue el envío: codprovincia-codmunicipio-codbarrio.\n");

                entity.Property(e => e.CodBarrio)
                    .HasColumnType("character varying")
                    .HasColumnName("cod_barrio")
                    .HasComment("Código que identifica de forma unívoca el barrio.\n");

                entity.Property(e => e.CodMunicipio)
                    .HasColumnType("character varying")
                    .HasColumnName("cod_municipio")
                    .HasComment("Código que identifica de forma unívoca el municipio.\n\n");

                entity.Property(e => e.CodProvincia)
                    .HasColumnType("character varying")
                    .HasColumnName("cod_provincia")
                    .HasComment("Código que identifica de forma unívoca a la provincia.\n");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id")
                    .HasComment("Identificador unívoco de la línea de distribucíon en el sistema.\n");

                entity.Property(e => e.MdDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("md_date")
                    .HasComment("Fecha en la que se define el grupo de inserción.\n");

                entity.Property(e => e.MdUuid)
                    .HasColumnType("character varying")
                    .HasColumnName("md_uuid")
                    .HasComment("Código de de metadato que indica el grupo de inserción al que pertenece el registro.\n\n");
            });

            modelBuilder.Entity<TdcTchEstadoPedido>(entity =>
            {
                entity.ToTable("tdc_tch_estado_pedidos", "dwh_torrecontrol");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasComment("Identificador unívoco del pedido en el sistema.\n");

                entity.Property(e => e.CodEstadoDevolucion)
                    .HasColumnType("character varying")
                    .HasColumnName("cod_estado_devolucion")
                    .HasComment("Código que identifica de forma unívoca el estado de devolución de un pedido.\n");

                entity.Property(e => e.CodEstadoEnvio)
                    .HasColumnType("character varying")
                    .HasColumnName("cod_estado_envio")
                    .HasComment("Código que identifica de forma unívoca el estado de envío de un pedido.\n");

                entity.Property(e => e.CodEstadoPago)
                    .HasColumnType("character varying")
                    .HasColumnName("cod_estado_pago")
                    .HasComment("Código que identifica de forma unívoca el estado de pago de un pedido.\n");

                entity.Property(e => e.CodLinea)
                    .HasColumnType("character varying")
                    .HasColumnName("cod_linea")
                    .HasComment("Código que identifica de forma unívoca la línea de distribución por carretera que sigue el envío: codprovincia-codmunicipio-codbarrio.\n");

                entity.Property(e => e.CodPedido)
                    .HasColumnType("character varying")
                    .HasColumnName("cod_pedido")
                    .HasComment("Código que identifica de forma unívoca un pedido. Se construye con: provincia-codfarmacia-id.\n");

                entity.Property(e => e.MdDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("md_date")
                    .HasComment("Fecha en la que se define el grupo de inserción.\n");

                entity.Property(e => e.MdUuid)
                    .HasColumnType("character varying")
                    .HasColumnName("md_uuid")
                    .HasComment("Código de metadato que indica el grupo de inserción al que pertenece el registro.");

                entity.HasOne(d => d.CodEstadoDevolucionNavigation)
                    .WithMany(p => p.TdcTchEstadoPedidos)
                    .HasForeignKey(d => d.CodEstadoDevolucion)
                    .HasConstraintName("cod_estado_devolucion_fk");

                entity.HasOne(d => d.CodEstadoEnvioNavigation)
                    .WithMany(p => p.TdcTchEstadoPedidos)
                    .HasForeignKey(d => d.CodEstadoEnvio)
                    .HasConstraintName("cod_estado_envio_fk");

                entity.HasOne(d => d.CodEstadoPagoNavigation)
                    .WithMany(p => p.TdcTchEstadoPedidos)
                    .HasForeignKey(d => d.CodEstadoPago)
                    .HasConstraintName("cod_estado_pago_fk");

                entity.HasOne(d => d.CodLineaNavigation)
                    .WithMany(p => p.TdcTchEstadoPedidos)
                    .HasForeignKey(d => d.CodLinea)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cod_linea_fk");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

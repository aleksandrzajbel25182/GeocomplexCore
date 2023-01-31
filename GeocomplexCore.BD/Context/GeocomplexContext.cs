using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace GeocomplexCore.DAL.Context
{
    public partial class GeocomplexContext : DbContext
    {
        public GeocomplexContext()
        {
        }

        public GeocomplexContext(DbContextOptions<GeocomplexContext> options)
            : base(options)
        {
        }

        public virtual DbSet<District> Districts { get; set; } = null!;
        public virtual DbSet<DistrictPoint> DistrictPoints { get; set; } = null!;
        public virtual DbSet<Egp> Egps { get; set; } = null!;
        public virtual DbSet<Ground> Grounds { get; set; } = null!;
        public virtual DbSet<GuideBreed> GuideBreeds { get; set; } = null!;
        public virtual DbSet<GuideClaritywater> GuideClaritywaters { get; set; } = null!;
        public virtual DbSet<GuideColor> GuideColors { get; set; } = null!;
        public virtual DbSet<GuideDensityBush> GuideDensityBushes { get; set; } = null!;
        public virtual DbSet<GuideEgpelement> GuideEgpelements { get; set; } = null!;
        public virtual DbSet<GuideForestDensity> GuideForestDensities { get; set; } = null!;
        public virtual DbSet<GuideFormareliefa> GuideFormareliefas { get; set; } = null!;
        public virtual DbSet<GuideFormariver> GuideFormarivers { get; set; } = null!;
        public virtual DbSet<GuideGroupprocce> GuideGroupprocces { get; set; } = null!;
        public virtual DbSet<GuideHeightUndergrowth> GuideHeightUndergrowths { get; set; } = null!;
        public virtual DbSet<GuideHeightreliefa> GuideHeightreliefas { get; set; } = null!;
        public virtual DbSet<GuideHumanimpact> GuideHumanimpacts { get; set; } = null!;
        public virtual DbSet<GuidePlant> GuidePlants { get; set; } = null!;
        public virtual DbSet<GuideProjcoverGroundcover> GuideProjcoverGroundcovers { get; set; } = null!;
        public virtual DbSet<GuideProjcoverUndergrowth> GuideProjcoverUndergrowths { get; set; } = null!;
        public virtual DbSet<GuideSanitarycondition> GuideSanitaryconditions { get; set; } = null!;
        public virtual DbSet<GuideSlope> GuideSlopes { get; set; } = null!;
        public virtual DbSet<GuideSmellwater> GuideSmellwaters { get; set; } = null!;
        public virtual DbSet<GuideSprexposition> GuideSprexpositions { get; set; } = null!;
        public virtual DbSet<GuideSubtypereliefa> GuideSubtypereliefas { get; set; } = null!;
        public virtual DbSet<GuideTastewater> GuideTastewaters { get; set; } = null!;
        public virtual DbSet<GuideTuperaid> GuideTuperaids { get; set; } = null!;
        public virtual DbSet<GuideTypePlant> GuideTypePlants { get; set; } = null!;
        public virtual DbSet<GuideTypebottom> GuideTypebottoms { get; set; } = null!;
        public virtual DbSet<GuideTypebreed> GuideTypebreeds { get; set; } = null!;
        public virtual DbSet<GuideTypeprocess> GuideTypeprocesses { get; set; } = null!;
        public virtual DbSet<GuideTypereliefa> GuideTypereliefas { get; set; } = null!;
        public virtual DbSet<GuideTypeusewater> GuideTypeusewaters { get; set; } = null!;
        public virtual DbSet<GuideVidprocess> GuideVidprocesses { get; set; } = null!;
        public virtual DbSet<GuideWaterseepage> GuideWaterseepages { get; set; } = null!;
        public virtual DbSet<GyideTypewatercourse> GyideTypewatercourses { get; set; } = null!;
        public virtual DbSet<Location> Locations { get; set; } = null!;
        public virtual DbSet<Organization> Organizations { get; set; } = null!;
        public virtual DbSet<PhotoWaterintake> PhotoWaterintakes { get; set; } = null!;
        public virtual DbSet<Plant> Plants { get; set; } = null!;
        public virtual DbSet<Project> Projects { get; set; } = null!;
        public virtual DbSet<Route> Routes { get; set; } = null!;
        public virtual DbSet<StreetWaterpipe> StreetWaterpipes { get; set; } = null!;
        public virtual DbSet<Surfacewater> Surfacewaters { get; set; } = null!;
        public virtual DbSet<Techobject> Techobjects { get; set; } = null!;
        public virtual DbSet<UserDatum> UserData { get; set; } = null!;
        public virtual DbSet<Watchpoint> Watchpoints { get; set; } = null!;
        public virtual DbSet<WaterObjectDi> WaterObjectDis { get; set; } = null!;
        public virtual DbSet<Waterintake> Waterintakes { get; set; } = null!;
        public virtual DbSet<WaterintakeFence> WaterintakeFences { get; set; } = null!;
        public virtual DbSet<WaterpipeWtr> WaterpipeWtrs { get; set; } = null!;
        public virtual DbSet<Watertower> Watertowers { get; set; } = null!;
        public virtual DbSet<WellHydroCoordinate> WellHydroCoordinates { get; set; } = null!;
        public virtual DbSet<WellHydrogeological> WellHydrogeologicals { get; set; } = null!;
        public virtual DbSet<WellOtherUse> WellOtherUses { get; set; } = null!;
        public virtual DbSet<WpointCoordinate> WpointCoordinates { get; set; } = null!;
        public virtual DbSet<WtrFenceangele> WtrFenceangeles { get; set; } = null!;
        public virtual DbSet<WtrNearestObj> WtrNearestObjs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                if (!optionsBuilder.IsConfigured)
                {
                    var builder = new ConfigurationBuilder();
                    // установка пути к текущему каталогу
                    builder.SetBasePath(Directory.GetCurrentDirectory());
                    // получаем конфигурацию из файла appsettings.json
                    builder.AddJsonFile("appsetting.json");
                    // создаем конфигурацию
                    var config = builder.Build();
                    // получаем строку подключения
                    string connectionString = config.GetConnectionString("DefaultConnection");

                    optionsBuilder.UseNpgsql(connectionString);
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<District>(entity =>
            {
                entity.HasKey(e => e.IdDistrict)
                    .HasName("district_pkey");

                entity.ToTable("district");

                entity.HasComment("Участки");

                entity.Property(e => e.IdDistrict)
                    .HasColumnName("Id_district")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(114L);

                entity.Property(e => e.DateAddDistrict).HasColumnName("date__add_district");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.NameDistrict)
                    .HasMaxLength(300)
                    .HasColumnName("name_district");

                entity.Property(e => e.PrgId).HasColumnName("prg_id");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Districts)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("fk_userId");

                entity.HasOne(d => d.Prg)
                    .WithMany(p => p.Districts)
                    .HasForeignKey(d => d.PrgId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_project_id_dis");
            });

            modelBuilder.Entity<DistrictPoint>(entity =>
            {
                entity.HasKey(e => e.IdDisctrictPoint)
                    .HasName("district_points_pkey");

                entity.ToTable("district_points");

                entity.HasComment("Точки участка/координаты");

                entity.Property(e => e.IdDisctrictPoint)
                    .HasColumnName("id_disctrict_point")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(80L);

                entity.Property(e => e.DisctrictPointX).HasColumnName("disctrict_point_X");

                entity.Property(e => e.DisctrictPointY).HasColumnName("disctrict_point_Y");

                entity.Property(e => e.DisctrictPointZ).HasColumnName("disctrict_point_Z");

                entity.Property(e => e.IdDistrict).HasColumnName("Id_district");

                entity.HasOne(d => d.IdDistrictNavigation)
                    .WithMany(p => p.DistrictPoints)
                    .HasForeignKey(d => d.IdDistrict)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_id_distcrit_point");
            });

            modelBuilder.Entity<Egp>(entity =>
            {
                entity.ToTable("egp");

                entity.HasComment("ЭГП");

                entity.Property(e => e.EgpId)
                    .ValueGeneratedNever()
                    .HasColumnName("egp_id");

                entity.Property(e => e.DataEgp).HasColumnName("data_egp");

                entity.Property(e => e.EgpArea)
                    .HasColumnName("egp_area")
                    .HasComment("площадь");

                entity.Property(e => e.EgpDeep)
                    .HasColumnName("egp_deep")
                    .HasComment("глубина");

                entity.Property(e => e.EgpDescription)
                    .HasMaxLength(5000)
                    .HasColumnName("egp_description");

                entity.Property(e => e.EgpLength)
                    .HasColumnName("egp_length")
                    .HasComment("протяженность процесса");

                entity.Property(e => e.EgpSpeed)
                    .HasColumnName("egp_speed")
                    .HasComment("Cкорость развития процесса");

                entity.Property(e => e.EgpVolume)
                    .HasColumnName("egp_volume")
                    .HasComment("объем");

                entity.Property(e => e.EgpWidth)
                    .HasColumnName("egp_width")
                    .HasComment("ширина");

                entity.Property(e => e.FEgpelement).HasColumnName("f_egpelement");

                entity.Property(e => e.FGroupprocess).HasColumnName("f_groupprocess");

                entity.Property(e => e.FTypeprocess).HasColumnName("f_typeprocess");

                entity.Property(e => e.FUserId).HasColumnName("f_user_id");

                entity.Property(e => e.FVidprocess).HasColumnName("f_vidprocess");

                entity.Property(e => e.FWpointId).HasColumnName("f_wpoint_id");

                entity.HasOne(d => d.FEgpelementNavigation)
                    .WithMany(p => p.Egps)
                    .HasForeignKey(d => d.FEgpelement)
                    .HasConstraintName("f_egpelement_id");

                entity.HasOne(d => d.FGroupprocessNavigation)
                    .WithMany(p => p.Egps)
                    .HasForeignKey(d => d.FGroupprocess)
                    .HasConstraintName("f_groupprocess_id");

                entity.HasOne(d => d.FTypeprocessNavigation)
                    .WithMany(p => p.Egps)
                    .HasForeignKey(d => d.FTypeprocess)
                    .HasConstraintName("f_typeprocess_id");

                entity.HasOne(d => d.FUser)
                    .WithMany(p => p.Egps)
                    .HasForeignKey(d => d.FUserId)
                    .HasConstraintName("f_user_id");

                entity.HasOne(d => d.FVidprocessNavigation)
                    .WithMany(p => p.Egps)
                    .HasForeignKey(d => d.FVidprocess)
                    .HasConstraintName("f_vid_process_id");

                entity.HasOne(d => d.FWpoint)
                    .WithMany(p => p.Egps)
                    .HasForeignKey(d => d.FWpointId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("f_wpoint_id");
            });

            modelBuilder.Entity<Ground>(entity =>
            {
                entity.HasKey(e => e.IdGround)
                    .HasName("ground_pkey");

                entity.ToTable("ground");

                entity.HasComment("Почва и грунт");

                entity.Property(e => e.IdGround)
                    .ValueGeneratedNever()
                    .HasColumnName("id_ground");

                entity.Property(e => e.DataGround).HasColumnName("data_ground");

                entity.Property(e => e.DescriptionGround)
                    .HasMaxLength(1000)
                    .HasColumnName("description_ground");

                entity.Property(e => e.FBreedId).HasColumnName("f_breed_id");

                entity.Property(e => e.FColor).HasColumnName("f_color");

                entity.Property(e => e.FDopcolor)
                    .HasMaxLength(20)
                    .HasColumnName("f_dopcolor");

                entity.Property(e => e.FUserId).HasColumnName("f_user_id");

                entity.Property(e => e.FWpointId).HasColumnName("f_wpoint_id");

                entity.Property(e => e.FromGround).HasColumnName("from_ground");

                entity.Property(e => e.ToGround).HasColumnName("to_ground");

                entity.HasOne(d => d.FBreed)
                    .WithMany(p => p.Grounds)
                    .HasForeignKey(d => d.FBreedId)
                    .HasConstraintName("fk_breed_id");

                entity.HasOne(d => d.FColorNavigation)
                    .WithMany(p => p.Grounds)
                    .HasForeignKey(d => d.FColor)
                    .HasConstraintName("fk_f_color");

                entity.HasOne(d => d.FUser)
                    .WithMany(p => p.Grounds)
                    .HasForeignKey(d => d.FUserId)
                    .HasConstraintName("fk_user_id");

                entity.HasOne(d => d.FWpoint)
                    .WithMany(p => p.Grounds)
                    .HasForeignKey(d => d.FWpointId)
                    .HasConstraintName("fk_wpoint_id");
            });

            modelBuilder.Entity<GuideBreed>(entity =>
            {
                entity.HasKey(e => e.IdBreed)
                    .HasName("guide.breed_pkey");

                entity.ToTable("guide.breed");

                entity.HasComment("Справочник породы");

                entity.Property(e => e.IdBreed)
                    .ValueGeneratedNever()
                    .HasColumnName("id_breed");

                entity.Property(e => e.FTypegroundId).HasColumnName("f_typeground_id");

                entity.Property(e => e.NameBreed)
                    .HasMaxLength(500)
                    .HasColumnName("name_breed");

                entity.Property(e => e.NamersBred)
                    .HasMaxLength(500)
                    .HasColumnName("namers_bred")
                    .HasComment("Окончание наименования");

                entity.HasOne(d => d.FTypeground)
                    .WithMany(p => p.GuideBreeds)
                    .HasForeignKey(d => d.FTypegroundId)
                    .HasConstraintName("fk_id_typebred");
            });

            modelBuilder.Entity<GuideClaritywater>(entity =>
            {
                entity.HasKey(e => e.IdClaritywater)
                    .HasName("guide.claritywater_pkey");

                entity.ToTable("guide.claritywater");

                entity.HasComment("Справочник Прозрачность воды");

                entity.Property(e => e.IdClaritywater)
                    .ValueGeneratedNever()
                    .HasColumnName("id_claritywater");

                entity.Property(e => e.NameClaritywater)
                    .HasMaxLength(100)
                    .HasColumnName("name_claritywater");
            });

            modelBuilder.Entity<GuideColor>(entity =>
            {
                entity.HasKey(e => e.IdColor)
                    .HasName("guide.color_pkey");

                entity.ToTable("guide.color");

                entity.HasComment("Справочник цвет");

                entity.Property(e => e.IdColor)
                    .ValueGeneratedNever()
                    .HasColumnName("id_color");

                entity.Property(e => e.BreedColor).HasColumnName("breed_color");

                entity.Property(e => e.NameColor)
                    .HasMaxLength(255)
                    .HasColumnName("name_color");

                entity.Property(e => e.PrimaryColor).HasColumnName("primary_color");

                entity.Property(e => e.SecondaryColor).HasColumnName("secondary_color");

                entity.Property(e => e.WaterColor).HasColumnName("water_color");
            });

            modelBuilder.Entity<GuideDensityBush>(entity =>
            {
                entity.HasKey(e => e.IdDbush)
                    .HasName("guide.density_bush_pkey");

                entity.ToTable("guide.density_bush");

                entity.HasComment("Справочник густота кустарников");

                entity.Property(e => e.IdDbush)
                    .ValueGeneratedNever()
                    .HasColumnName("id_dbush");

                entity.Property(e => e.NameDbush)
                    .HasMaxLength(50)
                    .HasColumnName("name_dbush");
            });

            modelBuilder.Entity<GuideEgpelement>(entity =>
            {
                entity.HasKey(e => e.IdEgpelement)
                    .HasName("guide.egpelement_pkey");

                entity.ToTable("guide.egpelement");

                entity.HasComment("Справочник Вторичный элемент ЭГП");

                entity.Property(e => e.IdEgpelement)
                    .ValueGeneratedNever()
                    .HasColumnName("id_egpelement");

                entity.Property(e => e.NameEgpelement)
                    .HasMaxLength(50)
                    .HasColumnName("name_egpelement");
            });

            modelBuilder.Entity<GuideForestDensity>(entity =>
            {
                entity.HasKey(e => e.IdDforest)
                    .HasName("guide.forest_density_pkey");

                entity.ToTable("guide.forest_density");

                entity.HasComment("Справочник густота леса");

                entity.Property(e => e.IdDforest)
                    .ValueGeneratedNever()
                    .HasColumnName("id_dforest");

                entity.Property(e => e.NameDforest)
                    .HasMaxLength(50)
                    .HasColumnName("name_dforest");
            });

            modelBuilder.Entity<GuideFormareliefa>(entity =>
            {
                entity.HasKey(e => e.IdFormareliefa)
                    .HasName(" guide.formareliefa_pkey");

                entity.ToTable("guide.formareliefa");

                entity.HasComment("Форма рельефа");

                entity.Property(e => e.IdFormareliefa)
                    .ValueGeneratedNever()
                    .HasColumnName("id_formareliefa");

                entity.Property(e => e.NameFormareliefa)
                    .HasMaxLength(100)
                    .HasColumnName("name_formareliefa");
            });

            modelBuilder.Entity<GuideFormariver>(entity =>
            {
                entity.HasKey(e => e.IdFormariver)
                    .HasName("guide.formariver_pkey");

                entity.ToTable("guide.formariver");

                entity.HasComment("Форма речной долины");

                entity.Property(e => e.IdFormariver)
                    .ValueGeneratedNever()
                    .HasColumnName("id_formariver");

                entity.Property(e => e.NameFormariver)
                    .HasMaxLength(100)
                    .HasColumnName("name_formariver");
            });

            modelBuilder.Entity<GuideGroupprocce>(entity =>
            {
                entity.HasKey(e => e.IdGroupprocces)
                    .HasName("guide.groupprocces_pkey");

                entity.ToTable("guide.groupprocces");

                entity.HasComment("Справочник группы процессов ЭГП");

                entity.Property(e => e.IdGroupprocces)
                    .ValueGeneratedNever()
                    .HasColumnName("id_groupprocces");

                entity.Property(e => e.NameGroupprocess)
                    .HasMaxLength(100)
                    .HasColumnName("name_groupprocess");
            });

            modelBuilder.Entity<GuideHeightUndergrowth>(entity =>
            {
                entity.HasKey(e => e.IdHeight)
                    .HasName("guide.height_undergrowth_pkey");

                entity.ToTable("guide.height_undergrowth");

                entity.HasComment("Справочник высоты подроста");

                entity.Property(e => e.IdHeight)
                    .ValueGeneratedNever()
                    .HasColumnName("id_height");

                entity.Property(e => e.NameHeight)
                    .HasMaxLength(50)
                    .HasColumnName("name_height");
            });

            modelBuilder.Entity<GuideHeightreliefa>(entity =>
            {
                entity.HasKey(e => e.IdHeightreliefa)
                    .HasName("guide.heightreliefa_pkey");

                entity.ToTable("guide.heightreliefa");

                entity.HasComment("Высотность рельефа");

                entity.Property(e => e.IdHeightreliefa)
                    .ValueGeneratedNever()
                    .HasColumnName("id_heightreliefa");

                entity.Property(e => e.NameHeightreliefa)
                    .HasMaxLength(100)
                    .HasColumnName("name_heightreliefa");
            });

            modelBuilder.Entity<GuideHumanimpact>(entity =>
            {
                entity.HasKey(e => e.IdHumanimpact)
                    .HasName("guide.humanimpact_pkey");

                entity.ToTable("guide.humanimpact");

                entity.HasComment("Справочник Антропогенное воздействие");

                entity.Property(e => e.IdHumanimpact)
                    .ValueGeneratedNever()
                    .HasColumnName("id_humanimpact");

                entity.Property(e => e.NameHumanimpact)
                    .HasMaxLength(100)
                    .HasColumnName("name_humanimpact");
            });

            modelBuilder.Entity<GuidePlant>(entity =>
            {
                entity.HasKey(e => e.IdPlant)
                    .HasName("guide.plants_pkey");

                entity.ToTable("guide.plants");

                entity.HasComment("Справочник растительности");

                entity.Property(e => e.IdPlant)
                    .ValueGeneratedNever()
                    .HasColumnName("id_plant");

                entity.Property(e => e.FTypePlant)
                    .HasColumnName("f_type_plant")
                    .HasComment("ID тип растительности");

                entity.Property(e => e.NamePlant)
                    .HasMaxLength(100)
                    .HasColumnName("name_plant");

                entity.HasOne(d => d.FTypePlantNavigation)
                    .WithMany(p => p.GuidePlants)
                    .HasForeignKey(d => d.FTypePlant)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("f_id_type_plant");
            });

            modelBuilder.Entity<GuideProjcoverGroundcover>(entity =>
            {
                entity.HasKey(e => e.IdPrjGround)
                    .HasName("guide.projcover_groundcover_pkey");

                entity.ToTable("guide.projcover_groundcover");

                entity.HasComment("Справочник проективное покрытие напочвенный покров(трава)");

                entity.Property(e => e.IdPrjGround)
                    .ValueGeneratedNever()
                    .HasColumnName("id_prj_ground");

                entity.Property(e => e.NamePrjGround)
                    .HasMaxLength(50)
                    .HasColumnName("name_prj_ground");
            });

            modelBuilder.Entity<GuideProjcoverUndergrowth>(entity =>
            {
                entity.HasKey(e => e.IdPrjUnder)
                    .HasName("guide.projcover_undergrowth_pkey");

                entity.ToTable("guide.projcover_undergrowth");

                entity.HasComment("Справочник проективное покрытие подроста");

                entity.Property(e => e.IdPrjUnder)
                    .ValueGeneratedNever()
                    .HasColumnName("id_prj_under");

                entity.Property(e => e.NamePrjUnder)
                    .HasMaxLength(50)
                    .HasColumnName("name_prj_under");
            });

            modelBuilder.Entity<GuideSanitarycondition>(entity =>
            {
                entity.HasKey(e => e.IdSanitar)
                    .HasName("guide.sanitarycondition_pkey");

                entity.ToTable("guide.sanitarycondition");

                entity.HasComment("Справочник санитарное состояние");

                entity.Property(e => e.IdSanitar)
                    .ValueGeneratedNever()
                    .HasColumnName("id_sanitar");

                entity.Property(e => e.NameSanitar)
                    .HasMaxLength(60)
                    .HasColumnName("name_sanitar");
            });

            modelBuilder.Entity<GuideSlope>(entity =>
            {
                entity.HasKey(e => e.IdSlope)
                    .HasName("guide.slope _pkey");

                entity.ToTable("guide.slope");

                entity.HasComment("Крутизна склона");

                entity.Property(e => e.IdSlope)
                    .ValueGeneratedNever()
                    .HasColumnName("id_slope ");

                entity.Property(e => e.NameSlope)
                    .HasMaxLength(100)
                    .HasColumnName("name_slope");
            });

            modelBuilder.Entity<GuideSmellwater>(entity =>
            {
                entity.HasKey(e => e.IdSmellwater)
                    .HasName("guide.smellwater_pkey");

                entity.ToTable("guide.smellwater");

                entity.HasComment("Справочник Запахи воды");

                entity.Property(e => e.IdSmellwater)
                    .ValueGeneratedNever()
                    .HasColumnName("id_smellwater");

                entity.Property(e => e.NameSmellwater)
                    .HasMaxLength(100)
                    .HasColumnName("name_smellwater");
            });

            modelBuilder.Entity<GuideSprexposition>(entity =>
            {
                entity.HasKey(e => e.IdSprexposition)
                    .HasName("guide.sprexposition_pkey");

                entity.ToTable("guide.sprexposition");

                entity.HasComment("Экспозиция");

                entity.Property(e => e.IdSprexposition)
                    .ValueGeneratedNever()
                    .HasColumnName("id_sprexposition");

                entity.Property(e => e.NameSprexposition)
                    .HasMaxLength(100)
                    .HasColumnName("name_sprexposition");
            });

            modelBuilder.Entity<GuideSubtypereliefa>(entity =>
            {
                entity.HasKey(e => e.IdSubtypereliefa)
                    .HasName("guide.subtypereliefa_pkey");

                entity.ToTable("guide.subtypereliefa");

                entity.HasComment("Подтип рельефа");

                entity.Property(e => e.IdSubtypereliefa)
                    .ValueGeneratedNever()
                    .HasColumnName("id_subtypereliefa");

                entity.Property(e => e.NameSubtypereliefa)
                    .HasColumnType("character varying")
                    .HasColumnName("name_subtypereliefa");
            });

            modelBuilder.Entity<GuideTastewater>(entity =>
            {
                entity.HasKey(e => e.IdTestwater)
                    .HasName("guide.tastewater_pkey");

                entity.ToTable("guide.tastewater");

                entity.HasComment("Справочник Вкус воды");

                entity.Property(e => e.IdTestwater)
                    .ValueGeneratedNever()
                    .HasColumnName("id_testwater");

                entity.Property(e => e.NameTestwater)
                    .HasMaxLength(100)
                    .HasColumnName("name_testwater");
            });

            modelBuilder.Entity<GuideTuperaid>(entity =>
            {
                entity.HasKey(e => e.IdTuperaid)
                    .HasName("guide.tuperaid_pkey");

                entity.ToTable("guide.tuperaid");

                entity.HasComment("Справочник Тип Налетов");

                entity.Property(e => e.IdTuperaid)
                    .ValueGeneratedNever()
                    .HasColumnName("id_tuperaid");

                entity.Property(e => e.NameTuperaid)
                    .HasColumnType("character varying")
                    .HasColumnName("name_tuperaid");
            });

            modelBuilder.Entity<GuideTypePlant>(entity =>
            {
                entity.HasKey(e => e.IdTypePlant)
                    .HasName("guide.type_plant_pkey");

                entity.ToTable("guide.type_plant");

                entity.HasComment("Справочник тип растительности");

                entity.Property(e => e.IdTypePlant)
                    .ValueGeneratedNever()
                    .HasColumnName("id_type_plant");

                entity.Property(e => e.NameTypePlant)
                    .HasMaxLength(50)
                    .HasColumnName("name_type_plant");
            });

            modelBuilder.Entity<GuideTypebottom>(entity =>
            {
                entity.HasKey(e => e.IdTypebottom)
                    .HasName("guide.typebottom_pkey");

                entity.ToTable("guide.typebottom");

                entity.HasComment("Справочник Тип дна");

                entity.Property(e => e.IdTypebottom)
                    .ValueGeneratedNever()
                    .HasColumnName("id_typebottom");

                entity.Property(e => e.NameTypebottom)
                    .HasMaxLength(200)
                    .HasColumnName("name_typebottom");
            });

            modelBuilder.Entity<GuideTypebreed>(entity =>
            {
                entity.HasKey(e => e.IdTypebreed)
                    .HasName("guide.typebreed_pkey");

                entity.ToTable("guide.typebreed");

                entity.HasComment("Тип породы");

                entity.Property(e => e.IdTypebreed)
                    .ValueGeneratedNever()
                    .HasColumnName("id_typebreed");

                entity.Property(e => e.NameTypebreed)
                    .HasMaxLength(100)
                    .HasColumnName("name_typebreed");
            });

            modelBuilder.Entity<GuideTypeprocess>(entity =>
            {
                entity.HasKey(e => e.IdTypeprocess)
                    .HasName("guide.typeprocess_pkey1");

                entity.ToTable("guide.typeprocess");

                entity.HasComment("Справочник Тип процесс ЭГП");

                entity.Property(e => e.IdTypeprocess)
                    .ValueGeneratedNever()
                    .HasColumnName("id_typeprocess");

                entity.Property(e => e.FGroupprocess).HasColumnName("f_groupprocess");

                entity.Property(e => e.NameTypeprocess)
                    .HasMaxLength(100)
                    .HasColumnName("name_typeprocess");
            });

            modelBuilder.Entity<GuideTypereliefa>(entity =>
            {
                entity.HasKey(e => e.IdTypereliefa)
                    .HasName("guide.typereliefa_pkey");

                entity.ToTable("guide.typereliefa");

                entity.HasComment("Тип рельефа");

                entity.Property(e => e.IdTypereliefa)
                    .ValueGeneratedNever()
                    .HasColumnName("id_typereliefa");

                entity.Property(e => e.NameTypereliefa)
                    .HasMaxLength(100)
                    .HasColumnName("name_typereliefa");
            });

            modelBuilder.Entity<GuideTypeusewater>(entity =>
            {
                entity.HasKey(e => e.IdTypeusewater)
                    .HasName("guide.typeusewater_pkey");

                entity.ToTable("guide.typeusewater");

                entity.HasComment("Справочник Тип использования");

                entity.Property(e => e.IdTypeusewater)
                    .ValueGeneratedNever()
                    .HasColumnName("id_typeusewater");

                entity.Property(e => e.NameTypeusewater)
                    .HasMaxLength(100)
                    .HasColumnName("name_typeusewater");
            });

            modelBuilder.Entity<GuideVidprocess>(entity =>
            {
                entity.HasKey(e => e.IdTypeprocess)
                    .HasName("guide.typeprocess_pkey");

                entity.ToTable("guide.vidprocess");

                entity.HasComment("Справочник Вид процесса ЭГП");

                entity.Property(e => e.IdTypeprocess)
                    .ValueGeneratedNever()
                    .HasColumnName("id_typeprocess");

                entity.Property(e => e.NameTypeprocess)
                    .HasMaxLength(50)
                    .HasColumnName("name_typeprocess");
            });

            modelBuilder.Entity<GuideWaterseepage>(entity =>
            {
                entity.HasKey(e => e.IdWaterseepage)
                    .HasName("guide.waterseepage_pkey");

                entity.ToTable("guide.waterseepage");

                entity.HasComment("Справочник Тип водопроявления");

                entity.Property(e => e.IdWaterseepage)
                    .ValueGeneratedNever()
                    .HasColumnName("id_waterseepage");

                entity.Property(e => e.NameWaterseepage)
                    .HasMaxLength(250)
                    .HasColumnName("name_waterseepage");
            });

            modelBuilder.Entity<GyideTypewatercourse>(entity =>
            {
                entity.HasKey(e => e.IdTypewatercourse)
                    .HasName("gyide.typewatercourse_pkey");

                entity.ToTable("gyide.typewatercourse");

                entity.HasComment("Справочник Тип водотока");

                entity.Property(e => e.IdTypewatercourse)
                    .ValueGeneratedNever()
                    .HasColumnName("id_typewatercourse");

                entity.Property(e => e.NameTypewatercourse)
                    .HasMaxLength(100)
                    .HasColumnName("name_typewatercourse");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.HasKey(e => e.LocId)
                    .HasName("location_pkey");

                entity.ToTable("location");

                entity.HasComment("Местоположение");

                entity.Property(e => e.LocId)
                    .HasColumnName("loc_id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.LocDistrict)
                    .HasMaxLength(255)
                    .HasColumnName("loc_district");

                entity.Property(e => e.LocLocality)
                    .HasMaxLength(255)
                    .HasColumnName("loc_locality");

                entity.Property(e => e.LocRegion)
                    .HasMaxLength(255)
                    .HasColumnName("loc_region");
            });

            modelBuilder.Entity<Organization>(entity =>
            {
                entity.HasKey(e => e.OrgId)
                    .HasName("drilling_organization_pkey");

                entity.ToTable("organization");

                entity.HasComment("Буровая организация");

                entity.Property(e => e.OrgId)
                    .HasColumnName("org_id")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(188L);

                entity.Property(e => e.OrgName)
                    .HasMaxLength(300)
                    .HasColumnName("org_name");

                entity.Property(e => e.OrgNote)
                    .HasMaxLength(500)
                    .HasColumnName("org_note");
            });

            modelBuilder.Entity<PhotoWaterintake>(entity =>
            {
                entity.ToTable("photo_waterintake");

                entity.HasComment("Фото водозабор");

                entity.Property(e => e.PhotoWaterintakeId)
                    .HasColumnName("photo_waterintake_id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.PhotoWaterintakePath)
                    .HasMaxLength(1000)
                    .HasColumnName("photo_waterintake_path");
            });

            modelBuilder.Entity<Plant>(entity =>
            {
                entity.ToTable("plants");

                entity.HasComment("Раститетельность");

                entity.Property(e => e.PlantId)
                    .ValueGeneratedNever()
                    .HasColumnName("plant_id");

                entity.Property(e => e.FUsrAdd).HasColumnName("f_usr_add");

                entity.Property(e => e.FWatchpoint).HasColumnName("f_watchpoint");

                entity.Property(e => e.PlantBush)
                    .HasMaxLength(25)
                    .HasColumnName("plant_bush")
                    .HasComment("Кустарник");

                entity.Property(e => e.PlantData)
                    .HasColumnName("plant_data")
                    .HasComment("Дата добавления");

                entity.Property(e => e.PlantDensityBush)
                    .HasColumnName("plant_density_bush")
                    .HasComment("Густота кустарников");

                entity.Property(e => e.PlantForestDensity)
                    .HasColumnName("plant_forest_density")
                    .HasComment("Густота леса");

                entity.Property(e => e.PlantGroundcover)
                    .HasMaxLength(50)
                    .HasColumnName("plant_groundcover")
                    .HasComment("Напочвенный покров (трава)");

                entity.Property(e => e.PlantHeightUndergrowth)
                    .HasColumnName("plant_height_undergrowth")
                    .HasComment("Высота подроста");

                entity.Property(e => e.PlantHumanimpact)
                    .HasMaxLength(50)
                    .HasColumnName("plant_humanimpact")
                    .HasComment("Антропогенное воздействие");

                entity.Property(e => e.PlantProjcoverGroundcover)
                    .HasColumnName("plant_projcover_groundcover")
                    .HasComment("Проективное покрытие напочвенный покров (трава)");

                entity.Property(e => e.PlantProjcoverUndergrowth)
                    .HasColumnName("plant_projcover_undergrowth")
                    .HasComment("Проективное покрытие подроста");

                entity.Property(e => e.PlantSanitarycondition)
                    .HasMaxLength(50)
                    .HasColumnName("plant_sanitarycondition")
                    .HasComment("Санитарное состояние");

                entity.Property(e => e.PlantSmallbush)
                    .HasMaxLength(50)
                    .HasColumnName("plant_smallbush")
                    .HasComment("Кустарничек");

                entity.Property(e => e.PlantStands)
                    .HasMaxLength(50)
                    .HasColumnName("plant_stands")
                    .HasComment("Древостой");

                entity.Property(e => e.PlantUndergrowth)
                    .HasMaxLength(50)
                    .HasColumnName("plant_undergrowth")
                    .HasComment("Подрост");

                entity.HasOne(d => d.FUsrAddNavigation)
                    .WithMany(p => p.Plants)
                    .HasForeignKey(d => d.FUsrAdd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("f_user_id");

                entity.HasOne(d => d.FWatchpointNavigation)
                    .WithMany(p => p.Plants)
                    .HasForeignKey(d => d.FWatchpoint)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("f_wpoint_id");

                entity.HasOne(d => d.PlantForestDensityNavigation)
                    .WithMany(p => p.Plants)
                    .HasForeignKey(d => d.PlantForestDensity)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("f_forest_density_id");

                entity.HasOne(d => d.PlantHeightUndergrowthNavigation)
                    .WithMany(p => p.Plants)
                    .HasForeignKey(d => d.PlantHeightUndergrowth)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("f_height_undergrowth_id");

                entity.HasOne(d => d.PlantProjcoverGroundcoverNavigation)
                    .WithMany(p => p.Plants)
                    .HasForeignKey(d => d.PlantProjcoverGroundcover)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("f_projcover_groundcover");

                entity.HasOne(d => d.PlantProjcoverUndergrowthNavigation)
                    .WithMany(p => p.Plants)
                    .HasForeignKey(d => d.PlantProjcoverUndergrowth)
                    .HasConstraintName("f_projcover_undergrowth_id");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(e => e.PrgId)
                    .HasName("progect_pkey");

                entity.ToTable("project");

                entity.HasComment("Проект");

                entity.Property(e => e.PrgId)
                    .HasColumnName("prg_id")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(59L);

                entity.Property(e => e.PrgDate).HasColumnName("prg_date");

                entity.Property(e => e.PrgName)
                    .HasMaxLength(255)
                    .HasColumnName("prg_name");

                entity.Property(e => e.PrgOrganization).HasColumnName("prg_organization");

                entity.HasOne(d => d.PrgOrganizationNavigation)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.PrgOrganization)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_organization_id");
            });

            modelBuilder.Entity<Route>(entity =>
            {
                entity.ToTable("route");

                entity.HasComment("Маршрут");

                entity.Property(e => e.RouteId)
                    .HasColumnName("route_id")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(346L);

                entity.Property(e => e.IdDistrict).HasColumnName("Id_district");

                entity.Property(e => e.RouteData).HasColumnName("route_data");

                entity.Property(e => e.RouteName)
                    .HasMaxLength(255)
                    .HasColumnName("route_name");

                entity.Property(e => e.RouteNote)
                    .HasMaxLength(4000)
                    .HasColumnName("route_note");

                entity.Property(e => e.Settlement).HasColumnName("settlement");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.IdDistrictNavigation)
                    .WithMany(p => p.Routes)
                    .HasForeignKey(d => d.IdDistrict)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_route_district");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Routes)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_route_user_data");
            });

            modelBuilder.Entity<StreetWaterpipe>(entity =>
            {
                entity.HasKey(e => e.StWtrpipeName)
                    .HasName("street_waterpipe_pkey");

                entity.ToTable("street_waterpipe");

                entity.HasComment("Улицы снабжаемые водоводом ");

                entity.Property(e => e.StWtrpipeName)
                    .HasMaxLength(300)
                    .HasColumnName("st_wtrpipe_name");

                entity.Property(e => e.StWtrpipeId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("st_wtrpipe_id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.WaterpipeWtrId).HasColumnName("waterpipe_wtr_id");

                entity.HasOne(d => d.WaterpipeWtr)
                    .WithMany(p => p.StreetWaterpipes)
                    .HasForeignKey(d => d.WaterpipeWtrId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_street_waterpipe");
            });

            modelBuilder.Entity<Surfacewater>(entity =>
            {
                entity.HasKey(e => e.SwId)
                    .HasName("surfacewater_pkey");

                entity.ToTable("surfacewater");

                entity.HasComment("Поверхностные воды");

                entity.Property(e => e.SwId)
                    .ValueGeneratedNever()
                    .HasColumnName("sw_id");

                entity.Property(e => e.FWpointId).HasColumnName("f_wpoint_id");

                entity.Property(e => e.SColorSecondary)
                    .HasMaxLength(255)
                    .HasColumnName("s_color_secondary")
                    .HasComment("Дополнительный цвет");

                entity.Property(e => e.SwAirtemp)
                    .HasColumnName("sw_airtemp")
                    .HasComment("Температура воздуха");

                entity.Property(e => e.SwBloom)
                    .HasColumnType("character varying")
                    .HasColumnName("sw_bloom")
                    .HasComment("Дополнительные параметры");

                entity.Property(e => e.SwClaritywaterId)
                    .HasColumnName("sw_claritywater_id")
                    .HasComment("Прозрачность воды");

                entity.Property(e => e.SwColorId)
                    .HasColumnName("sw_color_id")
                    .HasComment("Цвет");

                entity.Property(e => e.SwDateAdd).HasColumnName("sw_date_add");

                entity.Property(e => e.SwName)
                    .HasMaxLength(1000)
                    .HasColumnName("sw_name")
                    .HasComment("Название.Примечание");

                entity.Property(e => e.SwOdorwaterId)
                    .HasColumnName("sw_odorwater_id")
                    .HasComment("Запах");

                entity.Property(e => e.SwSpeedwater)
                    .HasColumnName("sw_speedwater")
                    .HasComment("Скорость течения");

                entity.Property(e => e.SwTastewaterId)
                    .HasColumnName("sw_tastewater_id")
                    .HasComment("Вкус воды");

                entity.Property(e => e.SwTypebottomId)
                    .HasColumnName("sw_typebottom_id")
                    .HasComment("Тип дна");

                entity.Property(e => e.SwTypewatercourseId)
                    .HasColumnName("sw_typewatercourse_id")
                    .HasComment("Тип водотока");

                entity.Property(e => e.SwWaterFlowRate)
                    .HasColumnName("sw_water_flow_rate")
                    .HasComment("Расход потока м 3");

                entity.Property(e => e.SwWatertemp)
                    .HasColumnName("sw_watertemp")
                    .HasComment("Температура воды");

                entity.Property(e => e.SwWidth)
                    .HasColumnName("sw_width")
                    .HasComment("Ширина русла");

                entity.Property(e => e.UserAddId).HasColumnName("user_add_id");

                entity.HasOne(d => d.FWpoint)
                    .WithMany(p => p.Surfacewaters)
                    .HasForeignKey(d => d.FWpointId)
                    .HasConstraintName("f_wpoint_id");

                entity.HasOne(d => d.SwColor)
                    .WithMany(p => p.Surfacewaters)
                    .HasForeignKey(d => d.SwColorId)
                    .HasConstraintName("f_color_id");

                entity.HasOne(d => d.SwOdorwater)
                    .WithMany(p => p.Surfacewaters)
                    .HasForeignKey(d => d.SwOdorwaterId)
                    .HasConstraintName("f_smellwater_id");

                entity.HasOne(d => d.SwTypebottom)
                    .WithMany(p => p.Surfacewaters)
                    .HasForeignKey(d => d.SwTypebottomId)
                    .HasConstraintName("f_typebottom_id");

                entity.HasOne(d => d.SwTypewatercourse)
                    .WithMany(p => p.Surfacewaters)
                    .HasForeignKey(d => d.SwTypewatercourseId)
                    .HasConstraintName("f_tupewatercource_id");

                entity.HasOne(d => d.UserAdd)
                    .WithMany(p => p.Surfacewaters)
                    .HasForeignKey(d => d.UserAddId)
                    .HasConstraintName("f_user_id");
            });

            modelBuilder.Entity<Techobject>(entity =>
            {
                entity.HasKey(e => e.TechobjId)
                    .HasName("techobject_pkey");

                entity.ToTable("techobject");

                entity.HasComment("Техногенный объект");

                entity.Property(e => e.TechobjId)
                    .ValueGeneratedNever()
                    .HasColumnName("techobj_id");

                entity.Property(e => e.FUserAdd).HasColumnName("f_user_add");

                entity.Property(e => e.FWpointId).HasColumnName("f_wpoint_id");

                entity.Property(e => e.TechobjAmountProducthole)
                    .HasColumnName("techobj_amount_producthole")
                    .HasComment("Количество эксплуатационных скважин");

                entity.Property(e => e.TechobjAmountWatchhole)
                    .HasColumnName("techobj_amount_watchhole")
                    .HasComment("Количество наблюдательных скважин  ");

                entity.Property(e => e.TechobjData).HasColumnName("techobj_data");

                entity.Property(e => e.TechobjDescription)
                    .HasMaxLength(1000)
                    .HasColumnName("techobj_description")
                    .HasComment("Примечание");

                entity.Property(e => e.TechobjLicense)
                    .HasMaxLength(255)
                    .HasColumnName("techobj_license")
                    .HasComment("Лицензия");

                entity.Property(e => e.TechobjMonitoring)
                    .HasColumnName("techobj_monitoring")
                    .HasComment("Наличие программы мониторинга качества воды: 0 - нет, 1 - есть\n");

                entity.Property(e => e.TechobjName)
                    .HasMaxLength(255)
                    .HasColumnName("techobj_name");

                entity.Property(e => e.TechobjProducthole)
                    .HasColumnName("techobj_producthole")
                    .HasComment("Наличие эксплуатационных скважин, родников, колодцев: 0 -нет, 1 - есть\n");

                entity.Property(e => e.TechobjSource)
                    .HasMaxLength(500)
                    .HasColumnName("techobj_source")
                    .HasComment("Источник техногенного воздействия");

                entity.Property(e => e.TechobjWatchhole)
                    .HasColumnName("techobj_watchhole")
                    .HasComment("Наличие наблюдательных скважин: 0 - нет, 1 - есть ");

                entity.HasOne(d => d.FUserAddNavigation)
                    .WithMany(p => p.Techobjects)
                    .HasForeignKey(d => d.FUserAdd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("f_user_id");

                entity.HasOne(d => d.FWpoint)
                    .WithMany(p => p.Techobjects)
                    .HasForeignKey(d => d.FWpointId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("f_watchpoint_id");
            });

            modelBuilder.Entity<UserDatum>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("user_data_pkey");

                entity.ToTable("user_data");

                entity.HasComment("Пользователи");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(48L);

                entity.Property(e => e.UserLogin)
                    .HasMaxLength(10)
                    .HasColumnName("user_login");

                entity.Property(e => e.UserName)
                    .HasMaxLength(40)
                    .HasColumnName("user_name");

                entity.Property(e => e.UserPassword)
                    .HasMaxLength(16)
                    .HasColumnName("user_password");

                entity.Property(e => e.UserRole)
                    .HasMaxLength(14)
                    .HasColumnName("user_role");
            });

            modelBuilder.Entity<Watchpoint>(entity =>
            {
                entity.HasKey(e => e.WpointId)
                    .HasName("watchpoint_pkey");

                entity.ToTable("watchpoint");

                entity.HasComment("Точка наблюдения");

                entity.Property(e => e.WpointId)
                    .HasColumnName("wpoint_id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.FUserId).HasColumnName("f_user_id");

                entity.Property(e => e.FWpointCoord).HasColumnName("f_wpoint_coord");

                entity.Property(e => e.IdExposition).HasColumnName("id_exposition");

                entity.Property(e => e.IdFormareliefa).HasColumnName("id_formareliefa");

                entity.Property(e => e.IdFormariver).HasColumnName("id_formariver");

                entity.Property(e => e.IdHeightreliefa).HasColumnName("id_heightreliefa");

                entity.Property(e => e.IdSlope).HasColumnName("id_slope");

                entity.Property(e => e.IdSubtypereliefa).HasColumnName("id_subtypereliefa");

                entity.Property(e => e.IdTypereliefa).HasColumnName("id_typereliefa");

                entity.Property(e => e.RouteId).HasColumnName("route_id");

                entity.Property(e => e.WpointDateAdd).HasColumnName("wpoint_date_add");

                entity.Property(e => e.WpointIndLandscape)
                    .HasMaxLength(300)
                    .HasColumnName("wpoint_ind_landscape");

                entity.Property(e => e.WpointLocation)
                    .HasMaxLength(200)
                    .HasColumnName("wpoint_location");

                entity.Property(e => e.WpointNote)
                    .HasMaxLength(4000)
                    .HasColumnName("wpoint_note");

                entity.Property(e => e.WpointNumber)
                    .HasMaxLength(20)
                    .HasColumnName("wpoint_number");

                entity.Property(e => e.WpointType)
                    .HasMaxLength(255)
                    .HasColumnName("wpoint_type");

                entity.HasOne(d => d.FUser)
                    .WithMany(p => p.Watchpoints)
                    .HasForeignKey(d => d.FUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_wpoint_user_id");

                entity.HasOne(d => d.IdExpositionNavigation)
                    .WithMany(p => p.Watchpoints)
                    .HasForeignKey(d => d.IdExposition)
                    .HasConstraintName("fk_wpoint_id_exposition");

                entity.HasOne(d => d.IdFormareliefaNavigation)
                    .WithMany(p => p.Watchpoints)
                    .HasForeignKey(d => d.IdFormareliefa)
                    .HasConstraintName("fk_wpoint_id_formareliefa");

                entity.HasOne(d => d.IdFormariverNavigation)
                    .WithMany(p => p.Watchpoints)
                    .HasForeignKey(d => d.IdFormariver)
                    .HasConstraintName("fk_wpoint_id_formariver ");

                entity.HasOne(d => d.IdHeightreliefaNavigation)
                    .WithMany(p => p.Watchpoints)
                    .HasForeignKey(d => d.IdHeightreliefa)
                    .HasConstraintName("fk_wpoint_id_heightreliefa");

                entity.HasOne(d => d.IdSlopeNavigation)
                    .WithMany(p => p.Watchpoints)
                    .HasForeignKey(d => d.IdSlope)
                    .HasConstraintName("fk_wpoint_id_slope");

                entity.HasOne(d => d.IdSubtypereliefaNavigation)
                    .WithMany(p => p.Watchpoints)
                    .HasForeignKey(d => d.IdSubtypereliefa)
                    .HasConstraintName("fk_wpoint_id_subtypereliefa");

                entity.HasOne(d => d.IdTypereliefaNavigation)
                    .WithMany(p => p.Watchpoints)
                    .HasForeignKey(d => d.IdTypereliefa)
                    .HasConstraintName("fk_wpoint_id_typereliefa");

                entity.HasOne(d => d.Route)
                    .WithMany(p => p.Watchpoints)
                    .HasForeignKey(d => d.RouteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_wpoint_route");
            });

            modelBuilder.Entity<WaterObjectDi>(entity =>
            {
                entity.HasKey(e => e.WtrObjDisName)
                    .HasName("water_object_dis_pkey");

                entity.ToTable("water_object_dis");

                entity.HasComment("Водозабор. Водные объекты в радиусе 1км");

                entity.Property(e => e.WtrObjDisName)
                    .HasMaxLength(255)
                    .HasColumnName("wtr_obj_dis_name");

                entity.Property(e => e.WtrIntakeId).HasColumnName("wtr_intake_id");

                entity.Property(e => e.WtrObjDisDistance).HasColumnName("wtr_obj_dis_distance");

                entity.Property(e => e.WtrObjDisId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("wtr_obj_dis_id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.WtrObjDisLoc)
                    .HasMaxLength(255)
                    .HasColumnName("wtr_obj_dis_loc");

                entity.HasOne(d => d.WtrIntake)
                    .WithMany(p => p.WaterObjectDis)
                    .HasForeignKey(d => d.WtrIntakeId)
                    .HasConstraintName("fk_water_object_dis_wtr_intake");
            });

            modelBuilder.Entity<Waterintake>(entity =>
            {
                entity.HasKey(e => e.WtrIntakeId)
                    .HasName("waterintake_pkey");

                entity.ToTable("waterintake");

                entity.HasComment("Водозабор");

                entity.Property(e => e.WtrIntakeId)
                    .HasColumnName("wtr_intake_id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.PhotoWtrIntakeId).HasColumnName("photo_wtr_intake_id");

                entity.Property(e => e.WpointId).HasColumnName("wpoint_id");

                entity.Property(e => e.WtrIntakeColwell).HasColumnName("wtr_intake_colwell");

                entity.Property(e => e.WtrIntakeGendrainage)
                    .HasColumnName("wtr_intake_gendrainage")
                    .HasComment("Общий водоотбор");

                entity.Property(e => e.WtrIntakeLicense)
                    .HasMaxLength(100)
                    .HasColumnName("wtr_intake_license");

                entity.Property(e => e.WtrIntakeName)
                    .HasMaxLength(200)
                    .HasColumnName("wtr_intake_ name");

                entity.Property(e => e.WtrIntakeSubsoiluser)
                    .HasMaxLength(200)
                    .HasColumnName("wtr_intake_subsoiluser");

                entity.Property(e => e.СulvertId).HasColumnName("сulvert_id");

                entity.HasOne(d => d.PhotoWtrIntake)
                    .WithMany(p => p.Waterintakes)
                    .HasForeignKey(d => d.PhotoWtrIntakeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_waterintake_photo");
            });

            modelBuilder.Entity<WaterintakeFence>(entity =>
            {
                entity.HasKey(e => e.WtrFenceId)
                    .HasName("waterintake_fence_pkey");

                entity.ToTable("waterintake_fence");

                entity.HasComment("Водозабор ограждения");

                entity.Property(e => e.WtrFenceId)
                    .HasColumnName("wtr_fence_id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.WtrFAngeleId).HasColumnName("wtr_f_angele_id ");

                entity.Property(e => e.WtrIntakeId).HasColumnName("wtr_intake_id");

                entity.Property(e => e.WtrIntakeInstal).HasColumnName("wtr_intake_instal");

                entity.Property(e => e.WtrIntakeLength).HasColumnName("wtr_intake_length");

                entity.Property(e => e.WtrIntakeWidth).HasColumnName("wtr_intake_width");

                entity.HasOne(d => d.WtrFAngele)
                    .WithMany(p => p.WaterintakeFences)
                    .HasForeignKey(d => d.WtrFAngeleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_waterintake_fence_wtr_fenceangele");

                entity.HasOne(d => d.WtrIntake)
                    .WithMany(p => p.WaterintakeFences)
                    .HasForeignKey(d => d.WtrIntakeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_waterintake_fence_wtrIntake");
            });

            modelBuilder.Entity<WaterpipeWtr>(entity =>
            {
                entity.ToTable("waterpipe_wtr");

                entity.HasComment("Водовод на водозаборе");

                entity.Property(e => e.WaterpipeWtrId)
                    .HasColumnName("waterpipe_wtr_id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.WaterpipeWtrDiametrpipe).HasColumnName("waterpipe_wtr_diametrpipe");

                entity.Property(e => e.WaterpipeWtrLength).HasColumnName("waterpipe_wtr_length");

                entity.Property(e => e.WaterpipeWtrMaterialpipe).HasColumnName("waterpipe_wtr_materialpipe");

                entity.Property(e => e.WpointId).HasColumnName("wpoint_id");
            });

            modelBuilder.Entity<Watertower>(entity =>
            {
                entity.HasKey(e => e.WatertId)
                    .HasName("watertower_pkey");

                entity.ToTable("watertower");

                entity.HasComment("Водонапорная башня ");

                entity.Property(e => e.WatertId)
                    .HasColumnName("watert_id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.WatertDistwell).HasColumnName("watert_distwell");

                entity.Property(e => e.WatertVolume).HasColumnName("watert_volume");

                entity.Property(e => e.WtrIntakeId).HasColumnName("wtr_intake_id");

                entity.HasOne(d => d.WtrIntake)
                    .WithMany(p => p.Watertowers)
                    .HasForeignKey(d => d.WtrIntakeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_watertower_ wtr_intake");
            });

            modelBuilder.Entity<WellHydroCoordinate>(entity =>
            {
                entity.HasKey(e => e.WhydroCoordZ)
                    .HasName("well_hydro_coordinates_pkey");

                entity.ToTable("well_hydro_coordinates");

                entity.HasComment("Координаты скважины гидрогеологической");

                entity.Property(e => e.WhydroCoordZ).HasColumnName("whydro_coord_Z");

                entity.Property(e => e.WellHydroId).HasColumnName("well_hydro_id");

                entity.Property(e => e.WhydroCoordId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("whydro_coord_id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.WhydroCoordX).HasColumnName("whydro_coord_X");

                entity.Property(e => e.WhydroCoordY).HasColumnName("whydro_coord_Y");

                entity.HasOne(d => d.WellHydro)
                    .WithMany(p => p.WellHydroCoordinates)
                    .HasForeignKey(d => d.WellHydroId)
                    .HasConstraintName("fk_wellcoord_well_id");
            });

            modelBuilder.Entity<WellHydrogeological>(entity =>
            {
                entity.HasKey(e => e.WellHydroId)
                    .HasName("well_hydrogeological_pkey");

                entity.ToTable("well_hydrogeological");

                entity.HasComment("Скважина гидрогеологическая");

                entity.Property(e => e.WellHydroId)
                    .HasColumnName("well_hydro_id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.DrilOrgId).HasColumnName("dril_org_id");

                entity.Property(e => e.Waterintake).HasColumnName("waterintake");

                entity.Property(e => e.WellHydroAddres)
                    .HasMaxLength(300)
                    .HasColumnName("well_hydro_addres");

                entity.Property(e => e.WellHydroDateDrillingEnd).HasColumnName("well_hydro_date_drilling_end");

                entity.Property(e => e.WellHydroDateDrillingStart).HasColumnName("well_hydro_date_drilling_start");

                entity.Property(e => e.WellHydroDepth).HasColumnName("well_hydro_depth");

                entity.Property(e => e.WellHydroDiametr).HasColumnName("well_hydro_diametr");

                entity.Property(e => e.WellHydroGeoBinding)
                    .HasMaxLength(300)
                    .HasColumnName("well_hydro_geo_binding");

                entity.Property(e => e.WellHydroName)
                    .HasMaxLength(255)
                    .HasColumnName("well_hydro_name");

                entity.Property(e => e.WellHydroNote)
                    .HasMaxLength(500)
                    .HasColumnName("well_hydro_note");

                entity.Property(e => e.WellHydroPassport)
                    .HasMaxLength(255)
                    .HasColumnName("well_hydro_passport");

                entity.Property(e => e.WpointId).HasColumnName("wpoint_id");
            });

            modelBuilder.Entity<WellOtherUse>(entity =>
            {
                entity.HasKey(e => e.WellOtherUsNumber)
                    .HasName("well_other_use_pkey");

                entity.ToTable("well_other_use");

                entity.HasComment("Водозабор.Скважина других недропользователей");

                entity.Property(e => e.WellOtherUsNumber)
                    .ValueGeneratedNever()
                    .HasColumnName("well_other_us_number");

                entity.Property(e => e.OrgId).HasColumnName("org_id");

                entity.Property(e => e.WellOtherUsDepth).HasColumnName("well_other_us_depth");

                entity.Property(e => e.WellOtherUsId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("well_other_us_id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.WtrIntakeId).HasColumnName("wtr_intake_id");

                entity.HasOne(d => d.Org)
                    .WithMany(p => p.WellOtherUses)
                    .HasForeignKey(d => d.OrgId)
                    .HasConstraintName("fk_well_use_org");

                entity.HasOne(d => d.WtrIntake)
                    .WithMany(p => p.WellOtherUses)
                    .HasForeignKey(d => d.WtrIntakeId)
                    .HasConstraintName("fk_well_use_wtr_intake");
            });

            modelBuilder.Entity<WpointCoordinate>(entity =>
            {
                entity.HasKey(e => e.WpCoordinatesId)
                    .HasName("wpoint_coordinates_pkey");

                entity.ToTable("wpoint_coordinates");

                entity.HasComment("Координаты точки наблюдения");

                entity.Property(e => e.WpCoordinatesId)
                    .HasColumnName("wp_coordinates_id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.WpCoordinatesX).HasColumnName("wp_coordinates_X");

                entity.Property(e => e.WpCoordinatesY).HasColumnName("wp_coordinates_Y");

                entity.Property(e => e.WpCoordinatesZ).HasColumnName("wp_coordinates_Z");

                entity.Property(e => e.WpointId).HasColumnName("wpoint_id");

                entity.HasOne(d => d.Wpoint)
                    .WithMany(p => p.WpointCoordinates)
                    .HasForeignKey(d => d.WpointId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_wpointid");
            });

            modelBuilder.Entity<WtrFenceangele>(entity =>
            {
                entity.HasKey(e => e.WtrFAngeleId)
                    .HasName("wtr_fenceangele_pkey");

                entity.ToTable("wtr_fenceangele");

                entity.HasComment("Водозабор углы ограждения");

                entity.Property(e => e.WtrFAngeleId)
                    .HasColumnName("wtr_f_angele_id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.WtrFAngeleNumber).HasColumnName("wtr_f_angele_number");

                entity.Property(e => e.WtrFAngeleX).HasColumnName("wtr_f_angele_X");

                entity.Property(e => e.WtrFAngeleY).HasColumnName("wtr_f_angele_Y");

                entity.Property(e => e.WtrFAngeleZ).HasColumnName("wtr_f_angele_Z");

                entity.Property(e => e.WtrIntakeId).HasColumnName("wtr_intake_id");

                entity.HasOne(d => d.WtrIntake)
                    .WithMany(p => p.WtrFenceangeles)
                    .HasForeignKey(d => d.WtrIntakeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_wtr_fenceangele_wtr_intake");
            });

            modelBuilder.Entity<WtrNearestObj>(entity =>
            {
                entity.HasKey(e => e.WtrIntakeId)
                    .HasName("wtr_nearest_obj_pkey");

                entity.ToTable("wtr_nearest_obj");

                entity.HasComment("Водозабор. Ближайшие объекты");

                entity.Property(e => e.WtrIntakeId)
                    .ValueGeneratedNever()
                    .HasColumnName("wtr_intake_id");

                entity.Property(e => e.WtrNrstObjDescripLoc)
                    .HasMaxLength(500)
                    .HasColumnName("wtr_nrst_obj_descrip_loc");

                entity.Property(e => e.WtrNrstObjDist).HasColumnName("wtr_nrst_obj_dist");

                entity.Property(e => e.WtrNrstObjId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("wtr_nrst_obj_id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.WtrNrstObjView)
                    .HasMaxLength(255)
                    .HasColumnName("wtr_nrst_obj_view");

                entity.HasOne(d => d.WtrIntake)
                    .WithOne(p => p.WtrNearestObj)
                    .HasForeignKey<WtrNearestObj>(d => d.WtrIntakeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_wtr_obj");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

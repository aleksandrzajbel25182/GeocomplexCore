using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace GeocomplexCore.BD.Context
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
        public virtual DbSet<Ground> Grounds { get; set; } = null!;
        public virtual DbSet<GuideBreed> GuideBreeds { get; set; } = null!;
        public virtual DbSet<GuideColor> GuideColors { get; set; } = null!;
        public virtual DbSet<GuideFormareliefa> GuideFormareliefas { get; set; } = null!;
        public virtual DbSet<GuideFormariver> GuideFormarivers { get; set; } = null!;
        public virtual DbSet<GuideHeightreliefa> GuideHeightreliefas { get; set; } = null!;
        public virtual DbSet<GuideSlope> GuideSlopes { get; set; } = null!;
        public virtual DbSet<GuideSprexposition> GuideSprexpositions { get; set; } = null!;
        public virtual DbSet<GuideSubtypereliefa> GuideSubtypereliefas { get; set; } = null!;
        public virtual DbSet<GuideTypebreed> GuideTypebreeds { get; set; } = null!;
        public virtual DbSet<GuideTypereliefa> GuideTypereliefas { get; set; } = null!;
        public virtual DbSet<Location> Locations { get; set; } = null!;
        public virtual DbSet<Organization> Organizations { get; set; } = null!;
        public virtual DbSet<PhotoWaterintake> PhotoWaterintakes { get; set; } = null!;
        public virtual DbSet<Project> Projects { get; set; } = null!;
        public virtual DbSet<Route> Routes { get; set; } = null!;
        public virtual DbSet<StreetWaterpipe> StreetWaterpipes { get; set; } = null!;
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

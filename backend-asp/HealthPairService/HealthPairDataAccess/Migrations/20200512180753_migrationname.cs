using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using System.Diagnostics.CodeAnalysis;

namespace HealthPairDataAccess.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class migrationname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Facilities",
                columns: table => new
                {
                    FacilityId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FacilityName = table.Column<string>(maxLength: 80, nullable: false),
                    FacilityAddress1 = table.Column<string>(maxLength: 120, nullable: false),
                    FacilityCity = table.Column<string>(maxLength: 40, nullable: false),
                    FacilityState = table.Column<string>(maxLength: 40, nullable: false),
                    FacilityZipcode = table.Column<int>(nullable: false),
                    FacilityPhoneNumber = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facilities", x => x.FacilityId);
                });

            migrationBuilder.CreateTable(
                name: "Insurances",
                columns: table => new
                {
                    InsuranceId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InsuranceName = table.Column<string>(maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insurances", x => x.InsuranceId);
                });

            migrationBuilder.CreateTable(
                name: "Specialties",
                columns: table => new
                {
                    SpecialtyId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Specialty = table.Column<string>(maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialties", x => x.SpecialtyId);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    PatientId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InsuranceId = table.Column<int>(nullable: false),
                    IsAdmin = table.Column<bool>(nullable: false, defaultValue: false),
                    PatientFirstName = table.Column<string>(maxLength: 80, nullable: false),
                    PatientLastName = table.Column<string>(maxLength: 80, nullable: false),
                    PatientPassword = table.Column<string>(maxLength: 80, nullable: false),
                    PatientAddress1 = table.Column<string>(maxLength: 120, nullable: false),
                    PatientCity = table.Column<string>(maxLength: 40, nullable: false),
                    PatientState = table.Column<string>(maxLength: 40, nullable: false),
                    PatientZipcode = table.Column<int>(nullable: false),
                    PatientBirthDay = table.Column<DateTime>(nullable: false),
                    PatientPhoneNumber = table.Column<long>(nullable: false),
                    PatientEmail = table.Column<string>(maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.PatientId);
                    table.ForeignKey(
                        name: "FK_Patients_Insurances_InsuranceId",
                        column: x => x.InsuranceId,
                        principalTable: "Insurances",
                        principalColumn: "InsuranceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Providers",
                columns: table => new
                {
                    ProviderId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FacilityId = table.Column<int>(nullable: false),
                    SpecialtyId = table.Column<int>(nullable: false),
                    ProviderFirstName = table.Column<string>(maxLength: 80, nullable: false),
                    ProviderLastName = table.Column<string>(maxLength: 80, nullable: false),
                    ProviderPhoneNumber = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Providers", x => x.ProviderId);
                    table.ForeignKey(
                        name: "FK_Providers_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "FacilityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Providers_Specialties_SpecialtyId",
                        column: x => x.SpecialtyId,
                        principalTable: "Specialties",
                        principalColumn: "SpecialtyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    AppointmentId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PatientId = table.Column<int>(nullable: false),
                    ProviderId = table.Column<int>(nullable: false),
                    AppointmentDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.AppointmentId);
                    table.ForeignKey(
                        name: "FK_Appointments_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_Providers_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Providers",
                        principalColumn: "ProviderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InsuranceProviders",
                columns: table => new
                {
                    IPId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InsuranceId = table.Column<int>(nullable: false),
                    ProviderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceProviders", x => x.IPId);
                    table.ForeignKey(
                        name: "FK_InsuranceProviders_Insurances_InsuranceId",
                        column: x => x.InsuranceId,
                        principalTable: "Insurances",
                        principalColumn: "InsuranceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InsuranceProviders_Providers_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Providers",
                        principalColumn: "ProviderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Facilities",
                columns: new[] { "FacilityId", "FacilityAddress1", "FacilityCity", "FacilityName", "FacilityPhoneNumber", "FacilityState", "FacilityZipcode" },
                values: new object[,]
                {
                    { 1, "1700 James Bowie Drive", "Baytown", "Houston Methodist San Jacinto Hospital Alexander Campus", 2814208765L, "TX", 77520 },
                    { 15, "3101 North Tarrant Parkway", "Fort Worth", "Medical Center of Alliance", 8176391100L, "TX", 76177 },
                    { 14, "5500 Colleyville Boulevard", "Colleyville", "Baylor Emergency Medical Center", 2142946350L, "TX", 76034 },
                    { 13, "1776 North Us 287, Suite 100", "Mansfield", "Baylor Emergency Medical Center", 2142946300L, "TX", 76063 },
                    { 12, "12500 South Freeway, Suite 100", "Burleson", "Baylor Emergency Medical Center", 2142946250L, "TX", 76028 },
                    { 10, "1975 Alpha, Suite 100", "Rockwall", "Baylor Emergency Medical Center", 2142946200L, "TX", 75087 },
                    { 9, "1300 N Main Ave", "Big Lake", "Reagan Memorial Hospital", 3258842561L, "TX", 76932 },
                    { 11, "620 South Main, Suite 100", "Keller", "Baylor Emergency Medical Center", 2142946100L, "TX", 76248 },
                    { 7, "600 Elizabeth Street", "Corpus Christi", "Christus Spohn Hospital Corpus Christi Shoreline", 3619024690L, "TX", 78404 },
                    { 6, "25440 I 45 North", "The Woodlands", "Woodlands Specialty Hospita", 2816028160L, "TX", 77386 },
                    { 5, "4000 24th Street", "Lubbock", "Covenant Medical Center – Lakeside", 8067250536L, "TX", 79410 },
                    { 4, "215 Chisholm Trail", "Jacksboro", "Faith Community Hospital", 9405676633L, "TX", 76458 },
                    { 3, "301 West Expressway 83", "McAllen", "McCallen Medical Center", 9566324000L, "TX", 78590 },
                    { 2, "16750 Red Oak Drive", "Houston", "Providence Hospital of North Houston LLC", 2814537110L, "TX", 77090 },
                    { 8, "13725 Northwest Blvd", "Corpus Christi", "The Corpus Christi Medical Center – Northwest", 3617674300L, "TX", 78410 }
                });

            migrationBuilder.InsertData(
                table: "Insurances",
                columns: new[] { "InsuranceId", "InsuranceName" },
                values: new object[,]
                {
                    { 10, "EmblemHealth" },
                    { 11, "None (Cash)" },
                    { 9, "Sendero" },
                    { 8, "Celtic" },
                    { 7, "Christus" },
                    { 6, "Superior" },
                    { 5, "FirstCare" },
                    { 4, "Molina" },
                    { 3, "Community" },
                    { 2, "Humana" },
                    { 1, "Cigna" }
                });

            migrationBuilder.InsertData(
                table: "Specialties",
                columns: new[] { "SpecialtyId", "Specialty" },
                values: new object[,]
                {
                    { 13, "Anesthesiologist" },
                    { 12, "Pediatrician" },
                    { 11, "Orthopedist" },
                    { 10, "Otolaryngologist" },
                    { 9, "Endocrinologist" },
                    { 8, "Neurologist" },
                    { 4, "Urologist" },
                    { 6, "Psychiatrist" },
                    { 5, "Gastroenterologist" },
                    { 14, "Pulmonologist" },
                    { 3, "Cardiologist" },
                    { 2, "Dermatologist" },
                    { 1, "Ophthalmologist" },
                    { 7, "Internist" },
                    { 15, "Proctologist" }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "PatientId", "InsuranceId", "IsAdmin", "PatientAddress1", "PatientBirthDay", "PatientCity", "PatientEmail", "PatientFirstName", "PatientLastName", "PatientPassword", "PatientPhoneNumber", "PatientState", "PatientZipcode" },
                values: new object[,]
                {
                    { 1, 1, true, "8401 Ronnie St", new DateTime(2000, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "White Settlement", "ifawdry0@si.edu", "James", "Kraulter", "Password1", 7167787419L, "TX", 76108 },
                    { 6, 1, true, "123 Test Street", new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test City", "TestEmail@test.com", "TestPatientFirstName", "TestPatientLastName", "TestPassword", 1234567890L, "Test State", 12345 },
                    { 2, 3, true, "2500 Victory Ave", new DateTime(1988, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dallas", "wotimony1@shop-pro.jp", "Jared", "McDonald", "Password2", 3619033062L, "TX", 75201 }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "PatientId", "InsuranceId", "PatientAddress1", "PatientBirthDay", "PatientCity", "PatientEmail", "PatientFirstName", "PatientLastName", "PatientPassword", "PatientPhoneNumber", "PatientState", "PatientZipcode" },
                values: new object[,]
                {
                    { 3, 5, "5919 Peg Street", new DateTime(2002, 10, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Houston", "lsiward2@cnbc.com", "Zack", "Marter", "Password3", 2452488647L, "TX", 77092 },
                    { 4, 7, "10810 Spring Cypress Rd.", new DateTime(1999, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tomball", "mindgs3@microsoft.com", "Joseph", "Morel", "Password4", 8823701130L, "TX", 77375 }
                });

            migrationBuilder.InsertData(
                table: "Providers",
                columns: new[] { "ProviderId", "FacilityId", "ProviderFirstName", "ProviderLastName", "ProviderPhoneNumber", "SpecialtyId" },
                values: new object[,]
                {
                    {1, 1, "Ratner", "Jacobs", 7167787419L, 8},{2, 2, "Josh", "Howzer", 3619033062L, 14}, {3, 3, "Mathew", "Grater", 2452488647L, 4},{4, 4, "Mary", "Kraom", 8823701130L, 10},{5, 5, "Jares", "Korsher", 9016226625L, 4},{6, 6, "Maury", "Chitter", 2577977019L, 14},
                    {7, 7, "Philomena", "Capon", 2834986226L, 2},{8, 8, "Michail", "Minkin", 4487593448L, 11},{9, 9, "Boothe", "Gurrado", 9373233203L, 1},{10, 10, "Kendall", "Mulqueen", 9033544326L, 8}, {11, 11, "Onfre", "Grazier", 8503233858L, 13},{12, 12, "Mauricio", "Rowes", 3259067361L, 14},
                    {13, 13, "Elana", "Dollman", 8696441600L, 9},{14, 14, "Georg", "Yakunchikov", 3464020514L, 7}, {15, 15, "Nanny", "Stead", 7705745366L, 3},{16, 1, "Jorie", "Atwool", 9363687510L, 1},{17, 2, "Alfons", "Shee", 6445427083L, 1},{18, 3, "Netta", "Fincken", 5252048919L, 3},{19, 4, "Melli", "Hansford", 5024459183L, 12},
                    {20, 5, "Kathryne", "Pawlaczyk", 6981610466L, 14}, {21, 6, "Dex", "Rawstron", 5243227555L, 6},{22, 7, "Dorie", "O'Dreain", 5123716488L, 7},{23, 8, "Leonora", "Pitford", 2138363970L, 14},{24, 9, "Whitney", "Sevior", 6174183997L, 12},{25, 10, "Zora", "Paolucci", 1909364291L, 15},
                    {26, 11, "Jayson", "Wookey", 8982665744L, 3},{27, 12, "Rock", "Sharville", 5864186454L, 15},{28, 13, "Dani", "Broadwell", 4221047432L, 12}, {29, 14, "Virginia", "McAvinchey", 8769325946L, 13},{30, 15, "Neala", "Cianelli", 4504879923L, 15},{31, 6, "Pooh", "Florio", 3317751153L, 6},
                    {32, 14, "Portia", "Treadwell", 3609514708L, 14}, {33, 10, "Tucky", "Dreher", 1313561327L, 10},{34, 5, "Gardie", "Drakes", 6921919943L, 5},{35, 4, "Phillip", "Sharville", 9529171778L, 4},{36, 1, "Byrle", "Shuttleworth", 2717398518L, 1}, {37, 5, "Hy", "Hamflett", 1206169437L, 5},
                    {38, 6, "Eadie", "Taill", 7997088826L, 6},{39, 9, "Timi", "Kestian", 2894355254L, 9},{40, 11, "Gearalt", "Dows", 7618424391L, 11},{41, 8, "Karol", "Fenech", 5874706348L, 8},{42, 7, "Silvia", "Carde", 5437112607L, 7}, {43, 1, "Muffin", "Mullany", 4584129942L, 1}, {44, 5, "Giffie", "Midford", 6354012053L, 5},
                    {45, 1, "Franni", "Burges", 2329785415L, 1},{46, 13, "Leigh", "Gresley", 1463130321L, 13}, {47, 11, "Lexy", "Delacroix", 8797031924L, 11}, {48, 13, "Zedekiah", "Raffon", 3596597282L, 13}, {49, 4, "Quinta", "Weld", 7604598198L, 4}, {50, 14, "Adrianne", "Duker", 8395572932L, 14}, {51, 13, "Marley", "Liddell", 1197344123L, 13},
                    {52, 7, "Tony", "Milillo", 5831953734L, 7},{53, 9, "Pippo", "Spata", 6926001101L, 9}, {54, 12, "Archibaldo", "Bullivent", 7088132363L, 12}, {55, 15, "Ettore", "Tomovic", 5929605450L, 15},{56, 15, "Neile", "Musgrave", 9963600040L, 15},{57, 14, "Nancy", "Laxson", 6687396234L, 14},
                    {58, 2, "Kaitlin", "Ginie", 1919320404L, 2},{59, 15, "Constantia", "Raatz", 8873624783L, 15}, {60, 4, "Nil", "McBratney", 3041721866L, 4},{61, 11, "Angel", "Kopecka", 8842345483L, 11}, {62, 15, "Rosalie", "Tickel", 6061298948L, 15}, {63, 15, "Marj", "Cammomile", 7819305964L, 15}, {64, 15, "Christian", "Brackley", 5684989925L, 15},
                    {65, 15, "Delilah", "Thairs", 8278791498L, 15}, {66, 14, "Daphne", "Norcop", 5138563198L, 14}, {67, 8, "Camala", "Brockelsby", 7593467333L, 8}, {68, 8, "Barri", "Manners", 6072246442L, 8},{69, 11, "Elisha", "Locksley", 8927201306L, 11}, {70, 11, "Josh", "Castellino", 2277379941L, 11}
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "AppointmentId", "AppointmentDate", "PatientId", "ProviderId" },
                values: new object[,]
                {
                    { 3, new DateTime(2020, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2 },
                    { 1, new DateTime(2020, 4, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 1 },
                    { 2, new DateTime(2020, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 3 }
                });

            migrationBuilder.InsertData(
                table: "InsuranceProviders",
                columns: new[] { "IPId", "InsuranceId", "ProviderId" },
                values: new object[,]
                {
                    {1, 1, 1}, {2, 1, 2}, {3, 1, 3}, {4, 1, 4}, {5, 1, 5}, {6, 1, 6}, {7, 1, 7}, {8, 1, 8}, {9, 1, 9}, {10, 1, 10}, {11, 1, 11}, {12, 1, 12}, {13, 1, 13}, {14, 1, 14}, {15, 1, 15}, {16, 1, 16}, {17, 1, 17}, {18, 1, 18}, {19, 1, 19}, {20, 1, 20}, {21, 1, 21}, {22, 2, 22}, {23, 2, 23}, {24, 2, 24}, {25, 2, 25}, {26, 2, 26}, {27, 2, 27}, {28, 2, 28}, {29, 3, 29}, {30, 3, 30}, {31, 3, 31}, {32, 3, 32}, {33, 3, 33}, {34, 3, 34}, {35, 3, 35}, {36, 3, 36}, {37, 3, 37}, {38, 3, 38}, {39, 3, 39}, {40, 3, 40}, {41, 3, 41}, {42, 3, 42}, {43, 4, 43}, {44, 4, 44}, {45, 4, 45}, {46, 4, 46}, {47, 4, 47}, {48, 4, 48}, {49, 4, 49}, {50, 4, 50}, {51, 4, 51}, {52, 4, 52}, {53, 4, 53}, {54, 4, 54}, {55, 4, 55}, {56, 4, 56}, {57, 5, 57}, {58, 5, 58}, {59, 5, 59}, {60, 5, 60}, {61, 6, 61}, {62, 6, 62}, {63, 6, 63}, {64, 6, 64}, {65, 6, 65}, {66, 6, 66}, {67, 6, 67}, {68, 6, 68}, {69, 6, 69}, {70, 6, 70}, {76, 6, 1}, {77, 6, 2}, {78, 6, 3}, {79, 6, 4}, {80, 6, 5}, {81, 7, 6}, {82, 7, 7}, {83, 7, 8}, {84, 7, 9}, {85, 7, 10}, {86, 7, 11}, {87, 7, 12}, {88, 7, 13}, {89, 7, 14}, {90, 7, 15}, {91, 7, 16}, {92, 7, 17}, {93, 7, 18}, {94, 7, 19}, {95, 7, 20}, {96, 7, 21}, {97, 7, 22}, {98, 7, 23}, {99, 7, 24}, {100, 7, 25}, {101, 7, 26}, {102, 7, 27}, {103, 7, 28}, {104, 7, 29}, {105, 7, 30}, {106, 7, 31}, {107, 8, 32}, {108, 8, 33}, {109, 8, 34}, {110, 8, 35}, {111, 8, 36}, {112, 8, 37}, {113, 8, 38}, {114, 8, 39}, {115, 8, 40}, {116, 8, 41}, {117, 8, 42}, {118, 8, 43}, {119, 8, 44}, {120, 8, 45}, {121, 8, 46}, {122, 8, 47}, {123, 8, 48}, {124, 8, 49}, {125, 8, 50}, {126, 8, 51}, {127, 8, 52}, {128, 9, 53}, {129, 9, 54}, {130, 9, 55}, {131, 9, 56}, {132, 9, 57}, {133, 9, 58}, {134, 9, 59}, {135, 9, 60}, {136, 9, 61}, {137, 10, 62}, {138, 10, 63}, {139, 10, 64}, {140, 10, 65}, {141, 10, 66}, {142, 10, 67}, {143, 10, 68}, {144, 10, 69}, {145, 10, 70}, {151, 10, 10}, {152, 10, 11}, {153, 10, 21}, {154, 10, 70}, {155, 11, 1}, {156, 11, 2}, {157, 11, 3}, {158, 11, 4}, {159, 11, 5}, {160, 11, 6}, {161, 11, 7}, {162, 11, 8}, {163, 11, 9}, {164, 11, 10}, {165, 11, 11}, {166, 11, 12}, {167, 11, 13}, {168, 11, 14}, {169, 11, 15}, {170, 11, 16}, {171, 11, 17}, {172, 11, 18}, {173, 11, 19}, {174, 11, 20}, {175, 11, 21}, {176, 11, 22}, {177, 11, 23}, {178, 11, 24}, {179, 11, 25}, {180, 11, 26}, {181, 11, 27}, {182, 11, 28}, {183, 11, 29}, {184, 11, 30}, {185, 11, 31}, {186, 11, 32}, {187, 11, 33}, {188, 11, 34}, {189, 11, 35}, {190, 11, 36}, {191, 11, 37}, {192, 11, 38}, {193, 11, 39}, {194, 11, 40}, {195, 11, 41}, {196, 11, 42}, {197, 11, 43}, {198, 11, 44}, {199, 11, 45}, {200, 11, 46}, {201, 11, 47}, {202, 11, 48}, {203, 11, 49}, {204, 11, 50}, {205, 11, 51}, {206, 11, 52}, {207, 11, 53}, {208, 11, 54}, {209, 11, 55}, {210, 11, 56}, {211, 11, 57}, {212, 11, 58}, {213, 11, 59}, {214, 11, 60}, {215, 11, 61}, {216, 11, 62}, {217, 11, 63}, {218, 11, 64}, {219, 11, 65}, {220, 11, 66}, {221, 11, 67}, {222, 11, 68}, {223, 11, 69}, {224, 11, 70}
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ProviderId",
                table: "Appointments",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceProviders_InsuranceId",
                table: "InsuranceProviders",
                column: "InsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceProviders_ProviderId",
                table: "InsuranceProviders",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_InsuranceId",
                table: "Patients",
                column: "InsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_Providers_FacilityId",
                table: "Providers",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Providers_SpecialtyId",
                table: "Providers",
                column: "SpecialtyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "InsuranceProviders");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Providers");

            migrationBuilder.DropTable(
                name: "Insurances");

            migrationBuilder.DropTable(
                name: "Facilities");

            migrationBuilder.DropTable(
                name: "Specialties");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskSubtask.Ef.Migrations
{
    public partial class seedingPriority : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(table: "Priorities",
             columns: new[] { "PriorityId", "PriorityName" },
             values: new object[] { Guid.NewGuid().ToString(), "low" }
             );

            migrationBuilder.InsertData(table: "Priorities",
            columns: new[] { "PriorityId", "PriorityName" },
            values: new object[] { Guid.NewGuid().ToString(), "mid" }
            );

            migrationBuilder.InsertData(table: "Priorities",
            columns: new[] { "PriorityId", "PriorityName" },
            values: new object[] { Guid.NewGuid().ToString(), "high" }
             );

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete From [Priorities]");

        }
    }
}

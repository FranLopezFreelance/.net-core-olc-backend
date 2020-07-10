﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AddCreationDateCols : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "STudent",
                table: "Comment",
                newName: "Student");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Instructor",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Course",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Comment",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Instructor");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Comment");

            migrationBuilder.RenameColumn(
                name: "Student",
                table: "Comment",
                newName: "STudent");
        }
    }
}

﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DatVeXemPhim2023.Models;

public partial class QldatVeXemPhimContext : DbContext
{
    public QldatVeXemPhimContext()
    {
    }

    public QldatVeXemPhimContext(DbContextOptions<QldatVeXemPhimContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TComBo> TComBos { get; set; }

    public virtual DbSet<TDanhGium> TDanhGia { get; set; }

    public virtual DbSet<TDoAn> TDoAns { get; set; }

    public virtual DbSet<TDoUong> TDoUongs { get; set; }

    public virtual DbSet<TDoVat> TDoVats { get; set; }

    public virtual DbSet<TGhe> TGhes { get; set; }

    public virtual DbSet<THoaDon> THoaDons { get; set; }

    public virtual DbSet<TPhim> TPhims { get; set; }

    public virtual DbSet<TPhongChieu> TPhongChieus { get; set; }

    public virtual DbSet<TQuaLuuNiem> TQuaLuuNiems { get; set; }

    public virtual DbSet<TRapChieuPhim> TRapChieuPhims { get; set; }

    public virtual DbSet<TSuatChieu> TSuatChieus { get; set; }

    public virtual DbSet<TTaiKhoan> TTaiKhoans { get; set; }

    public virtual DbSet<TThanhToan> TThanhToans { get; set; }

    public virtual DbSet<TThongTinKhuyenMai> TThongTinKhuyenMais { get; set; }

    public virtual DbSet<TVe> TVes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-IQ4VCEEV;Initial Catalog=QLDatVeXemPhim;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TComBo>(entity =>
        {
            entity.HasKey(e => e.IdcomBo);

            entity.ToTable("tComBo");

            entity.Property(e => e.IdcomBo).HasColumnName("IDComBo");
            entity.Property(e => e.TenCombo).HasMaxLength(50);
        });

        modelBuilder.Entity<TDanhGium>(entity =>
        {
            entity.HasKey(e => e.IddanhGia);

            entity.ToTable("tDanhGia");

            entity.Property(e => e.IddanhGia).HasColumnName("IDDanhGia");
            entity.Property(e => e.DanhGiaCmt).HasMaxLength(200);
            entity.Property(e => e.Idphim).HasColumnName("IDPhim");

            entity.HasOne(d => d.IdphimNavigation).WithMany(p => p.TDanhGia)
                .HasForeignKey(d => d.Idphim)
                .HasConstraintName("FK_tDanhGia_tPhim");
        });

        modelBuilder.Entity<TDoAn>(entity =>
        {
            entity.HasKey(e => e.IddoAn);

            entity.ToTable("tDoAn");

            entity.Property(e => e.IddoAn).HasColumnName("IDDoAn");
            entity.Property(e => e.TenDoAn).HasMaxLength(50);
        });

        modelBuilder.Entity<TDoUong>(entity =>
        {
            entity.HasKey(e => e.IddoUong);

            entity.ToTable("tDoUong");

            entity.Property(e => e.IddoUong).HasColumnName("IDDoUong");
            entity.Property(e => e.TenDoUong).HasMaxLength(50);
        });

        modelBuilder.Entity<TDoVat>(entity =>
        {
            entity.HasKey(e => e.IddoVat).HasName("PK_tDoVat_1");

            entity.ToTable("tDoVat");

            entity.Property(e => e.IddoVat).HasColumnName("IDDoVat");
            entity.Property(e => e.IdcomBo).HasColumnName("IDComBo");
            entity.Property(e => e.IddoAn).HasColumnName("IDDoAn");
            entity.Property(e => e.IddoUong).HasColumnName("IDDoUong");
            entity.Property(e => e.IdquaLuuNiem).HasColumnName("IDQuaLuuNiem");
            entity.Property(e => e.IdrapChieuPhim).HasColumnName("IDRapChieuPhim");

            entity.HasOne(d => d.IdcomBoNavigation).WithMany(p => p.TDoVats)
                .HasForeignKey(d => d.IdcomBo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tDoVat_tComBo");

            entity.HasOne(d => d.IddoAnNavigation).WithMany(p => p.TDoVats)
                .HasForeignKey(d => d.IddoAn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tDoVat_tDoAn");

            entity.HasOne(d => d.IddoUongNavigation).WithMany(p => p.TDoVats)
                .HasForeignKey(d => d.IddoUong)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tDoVat_tDoUong");

            entity.HasOne(d => d.IdquaLuuNiemNavigation).WithMany(p => p.TDoVats)
                .HasForeignKey(d => d.IdquaLuuNiem)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tDoVat_tQuaLuuNiem");

            entity.HasOne(d => d.IdrapChieuPhimNavigation).WithMany(p => p.TDoVats)
                .HasForeignKey(d => d.IdrapChieuPhim)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tDoVat_tRapChieuPhim");
        });

        modelBuilder.Entity<TGhe>(entity =>
        {
            entity.HasKey(e => e.Idghe);

            entity.ToTable("tGhe");

            entity.Property(e => e.Idghe).HasColumnName("IDGhe");
            entity.Property(e => e.IdphongChieuPhim).HasColumnName("IDphongChieuPhim");

            entity.HasOne(d => d.IdphongChieuPhimNavigation).WithMany(p => p.TGhes)
                .HasForeignKey(d => d.IdphongChieuPhim)
                .HasConstraintName("FK_tGhe_tPhongChieu");
        });

        modelBuilder.Entity<THoaDon>(entity =>
        {
            entity.HasKey(e => e.IdhoaDon);

            entity.ToTable("tHoaDon");

            entity.Property(e => e.IdhoaDon).HasColumnName("IDHoaDon");
            entity.Property(e => e.IddoVat).HasColumnName("IDDoVat");
            entity.Property(e => e.Iduser).HasColumnName("IDUser");
            entity.Property(e => e.Idve).HasColumnName("IDVe");
            entity.Property(e => e.NgayLap).HasColumnType("datetime");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(10)
                .IsFixedLength();

            entity.HasOne(d => d.IduserNavigation).WithMany(p => p.THoaDons)
                .HasForeignKey(d => d.Iduser)
                .HasConstraintName("FK_tHoaDon_tTaiKhoan");
        });

        modelBuilder.Entity<TPhim>(entity =>
        {
            entity.HasKey(e => e.Idphim);

            entity.ToTable("tPhim");

            entity.Property(e => e.Idphim).HasColumnName("IDPhim");
            entity.Property(e => e.DaoDien).HasMaxLength(255);
            entity.Property(e => e.DienVien).HasMaxLength(255);
            entity.Property(e => e.HinhAnh)
                .HasMaxLength(200)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.MoTa).HasMaxLength(255);
            entity.Property(e => e.NgayCongChieu).HasColumnType("datetime");
            entity.Property(e => e.TenPhim).HasMaxLength(255);
            entity.Property(e => e.TheLoai).HasMaxLength(255);
            entity.Property(e => e.ThoiLuong).HasMaxLength(50);
        });

        modelBuilder.Entity<TPhongChieu>(entity =>
        {
            entity.HasKey(e => e.IdphongChieu);

            entity.ToTable("tPhongChieu");

            entity.Property(e => e.IdphongChieu).HasColumnName("IDphongChieu");
            entity.Property(e => e.IdrapChieuPhim).HasColumnName("IDRapChieuPhim");
            entity.Property(e => e.TenPhongChieu).HasMaxLength(50);

            entity.HasOne(d => d.IdrapChieuPhimNavigation).WithMany(p => p.TPhongChieus)
                .HasForeignKey(d => d.IdrapChieuPhim)
                .HasConstraintName("FK_tPhongChieu_tRapChieuPhim");
        });

        modelBuilder.Entity<TQuaLuuNiem>(entity =>
        {
            entity.HasKey(e => e.IdquaLuuNiem);

            entity.ToTable("tQuaLuuNiem");

            entity.Property(e => e.IdquaLuuNiem).HasColumnName("IDQuaLuuNiem");
            entity.Property(e => e.TenQuaLuuNiem).HasMaxLength(50);
        });

        modelBuilder.Entity<TRapChieuPhim>(entity =>
        {
            entity.HasKey(e => e.IdrapChieuPhim);

            entity.ToTable("tRapChieuPhim");

            entity.Property(e => e.IdrapChieuPhim).HasColumnName("IDRapChieuPhim");
            entity.Property(e => e.DiaChi).HasMaxLength(255);
            entity.Property(e => e.TenRap).HasMaxLength(255);
        });

        modelBuilder.Entity<TSuatChieu>(entity =>
        {
            entity.HasKey(e => e.IdsuatChieu);

            entity.ToTable("tSuatChieu");

            entity.Property(e => e.IdsuatChieu).HasColumnName("IDSuatChieu");
            entity.Property(e => e.GheTrong).HasMaxLength(255);
            entity.Property(e => e.Idphim).HasColumnName("IDPhim");
            entity.Property(e => e.IdrapChieuPhim).HasColumnName("IDRapChieuPhim");
            entity.Property(e => e.TgbatDau)
                .HasColumnType("datetime")
                .HasColumnName("TGBatDau");
            entity.Property(e => e.TgketThuc)
                .HasColumnType("datetime")
                .HasColumnName("TGKetThuc");

            entity.HasOne(d => d.IdphimNavigation).WithMany(p => p.TSuatChieus)
                .HasForeignKey(d => d.Idphim)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tSuatChieu_tPhim");

            entity.HasOne(d => d.IdrapChieuPhimNavigation).WithMany(p => p.TSuatChieus)
                .HasForeignKey(d => d.IdrapChieuPhim)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tSuatChieu_tRapChieuPhim");
        });

        modelBuilder.Entity<TTaiKhoan>(entity =>
        {
            entity.HasKey(e => e.Iduser);

            entity.ToTable("tTaiKhoan");

            entity.Property(e => e.Iduser).HasColumnName("IDUser");
            entity.Property(e => e.DiaChi).HasMaxLength(200);
            entity.Property(e => e.HoTen).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Sdt)
                .HasMaxLength(10)
                .HasColumnName("SDT");
            entity.Property(e => e.TypeUser).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        modelBuilder.Entity<TThanhToan>(entity =>
        {
            entity.HasKey(e => new { e.IdthanhToan, e.IdhoaDon });

            entity.ToTable("tThanhToan");

            entity.Property(e => e.IdthanhToan)
                .ValueGeneratedOnAdd()
                .HasColumnName("IDThanhToan");
            entity.Property(e => e.IdhoaDon).HasColumnName("IDHoaDon");
            entity.Property(e => e.NgayThanhToan)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Pttt)
                .HasMaxLength(255)
                .HasColumnName("PTTT");

            entity.HasOne(d => d.IdhoaDonNavigation).WithMany(p => p.TThanhToans)
                .HasForeignKey(d => d.IdhoaDon)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tThanhToan_tHoaDon");

            entity.HasOne(d => d.IdthanhToanNavigation).WithMany(p => p.TThanhToans)
                .HasForeignKey(d => d.IdthanhToan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tThanhToan_tVe");
        });

        modelBuilder.Entity<TThongTinKhuyenMai>(entity =>
        {
            entity.HasKey(e => e.IdkhuyenMai);

            entity.ToTable("tThongTinKhuyenMai");

            entity.Property(e => e.IdkhuyenMai).HasColumnName("IDKhuyenMai");
            entity.Property(e => e.Iduser).HasColumnName("IDUser");
            entity.Property(e => e.NdkhuyenMai)
                .HasMaxLength(255)
                .HasColumnName("NDKhuyenMai");
            entity.Property(e => e.TenKhuyenMai).HasMaxLength(255);
            entity.Property(e => e.TimeBegin).HasColumnType("datetime");
            entity.Property(e => e.TimeEnd).HasColumnType("datetime");

            entity.HasOne(d => d.IduserNavigation).WithMany(p => p.TThongTinKhuyenMais)
                .HasForeignKey(d => d.Iduser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tThongTinKhuyenMai_tTaiKhoan");
        });

        modelBuilder.Entity<TVe>(entity =>
        {
            entity.HasKey(e => e.Idve).HasName("PK_tVe_1");

            entity.ToTable("tVe");

            entity.Property(e => e.Idve).HasColumnName("IDVe");
            entity.Property(e => e.Idghe).HasColumnName("IDGhe");
            entity.Property(e => e.IdsuatChieu).HasColumnName("IDSuatChieu");
            entity.Property(e => e.IdthanhToan).HasColumnName("IDThanhToan");
            entity.Property(e => e.TgdatVe)
                .HasColumnType("datetime")
                .HasColumnName("TGDatVe");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(10)
                .IsFixedLength();

            entity.HasOne(d => d.IdsuatChieuNavigation).WithMany(p => p.TVes)
                .HasForeignKey(d => d.IdsuatChieu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tVe_tSuatChieu");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

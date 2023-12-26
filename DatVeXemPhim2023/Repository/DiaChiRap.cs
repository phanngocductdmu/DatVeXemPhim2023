using DatVeXemPhim2023.Models;
namespace DatVeXemPhim2023.Repository
{
    public interface DiaChiRap
    {
        TRapChieuPhim Add(TRapChieuPhim loairap);
        TRapChieuPhim Update(TRapChieuPhim loairap);
        TRapChieuPhim Delete(int idRapPhim);
        TRapChieuPhim GetDiaChi(int idRapPhim);
        IEnumerable<TRapChieuPhim> GetAllDiaChi();
    }
}

using DatVeXemPhim2023.Models;

namespace DatVeXemPhim2023.Repository
{
    public class DiaChiRepository : DiaChiRap
    {

        private readonly QldatVeXemPhimContext _context;
        public DiaChiRepository(QldatVeXemPhimContext context)
        {
            _context = context;
        }
        public TRapChieuPhim Add(TRapChieuPhim loairap)
        {
            _context.TRapChieuPhims.Add(loairap);
            _context.SaveChanges();
            return loairap;
        }

        public TRapChieuPhim Delete(int idRapPhim)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TRapChieuPhim> GetAllDiaChi()
        {
            return _context.TRapChieuPhims;
        }

        public TRapChieuPhim GetDiaChi(int idRapPhim)
        {
            return _context.TRapChieuPhims.Find(idRapPhim);
        }

        public TRapChieuPhim Update(TRapChieuPhim loairap)
        {
            _context.Update(loairap);
            _context.SaveChanges();
            return loairap;
        }
    }
}

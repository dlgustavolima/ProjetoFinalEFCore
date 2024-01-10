using Microsoft.EntityFrameworkCore.Diagnostics;

namespace EFCoreProjetoFinal.Data.Interceptors
{
    public class InterceptadorPersistencia : SaveChangesInterceptor
    {
        public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
        {
            Console.WriteLine(eventData.Context.ChangeTracker.DebugView.LongView);

            return base.SavedChanges(eventData, result);
        }


    }
}

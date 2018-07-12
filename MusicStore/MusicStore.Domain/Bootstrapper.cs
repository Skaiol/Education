using MusicStore.Dal;

namespace MusicStore.Domain
{
    public static class Bootstrapper
    {
        public static void Start()
        {
            using (var context = new MusicStoreContext())
            {
                context.Database.Initialize(false);
            }
        }
    }
}
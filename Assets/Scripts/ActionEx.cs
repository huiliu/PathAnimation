using System;

    public static class ActionEx
    {
        public static void SafeInvoke<T>(this Action<T> action, T t)
        {
            if (action == null)
                return;

            try
            {
                action.Invoke(t);
            }
            catch (Exception err)
            {
                System.Diagnostics.Debug.Assert(false, err.Message);
            }
        }
    }

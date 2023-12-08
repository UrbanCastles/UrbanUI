
namespace UrbanUI.WPF.Win32
{

   static class NativeUtils
   {
      internal static uint TPM_LEFTALIGN;

      internal static uint TPM_RETURNCMD;

      static NativeUtils()
      {
         NativeUtils.TPM_LEFTALIGN = 0;
         NativeUtils.TPM_RETURNCMD = 256;
      }
   }
}

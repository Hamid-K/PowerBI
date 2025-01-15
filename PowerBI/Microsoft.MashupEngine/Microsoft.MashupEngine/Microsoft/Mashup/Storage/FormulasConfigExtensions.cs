using System;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x02002077 RID: 8311
	public static class FormulasConfigExtensions
	{
		// Token: 0x0600CB64 RID: 52068 RVA: 0x002885BC File Offset: 0x002867BC
		public static FormulasConfig GetFormulasConfig(this PackagePartStorage packagePartStorage)
		{
			string text;
			byte[] array;
			if (!packagePartStorage.TryGetPartContent(PackagePartType.Config, "formulas.xml", out text, out array))
			{
				return new FormulasConfig();
			}
			FormulasConfig formulasConfig;
			if (!Xml<FormulasConfig>.TryDeserializeBytes(array, out formulasConfig))
			{
				throw new StorageException(Strings.Package_Unrecognized_File_Format);
			}
			return formulasConfig;
		}
	}
}

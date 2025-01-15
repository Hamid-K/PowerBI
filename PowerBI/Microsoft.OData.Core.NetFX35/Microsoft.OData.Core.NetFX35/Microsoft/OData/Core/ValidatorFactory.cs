using System;

namespace Microsoft.OData.Core
{
	// Token: 0x020002B7 RID: 695
	internal static class ValidatorFactory
	{
		// Token: 0x060017EF RID: 6127 RVA: 0x0005207A File Offset: 0x0005027A
		public static IWriterValidator CreateWriterValidator(bool enableFullValidation)
		{
			if (enableFullValidation)
			{
				IWriterValidator writerValidator;
				if ((writerValidator = ValidatorFactory.fullWriterValidator) == null)
				{
					writerValidator = (ValidatorFactory.fullWriterValidator = new WriterValidatorFullValidation());
				}
				return writerValidator;
			}
			IWriterValidator writerValidator2;
			if ((writerValidator2 = ValidatorFactory.minimalWriterValidator) == null)
			{
				writerValidator2 = (ValidatorFactory.minimalWriterValidator = new WriterValidatorMinimalValidation());
			}
			return writerValidator2;
		}

		// Token: 0x04000A40 RID: 2624
		private static IWriterValidator fullWriterValidator;

		// Token: 0x04000A41 RID: 2625
		private static IWriterValidator minimalWriterValidator;
	}
}

using System;
using System.Reflection;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001CA8 RID: 7336
	internal static class ExceptionInternals
	{
		// Token: 0x0600B655 RID: 46677 RVA: 0x00250489 File Offset: 0x0024E689
		public static bool TrySetStackTraceString(Exception e, string stackTrace)
		{
			return ExceptionInternals.TrySetField(e, "_stackTraceString", stackTrace, ref ExceptionInternals.stackTraceField);
		}

		// Token: 0x0600B656 RID: 46678 RVA: 0x0025049C File Offset: 0x0024E69C
		public static bool TrySetClassName(Exception e, string className)
		{
			return ExceptionInternals.TrySetField(e, "_className", className, ref ExceptionInternals.classNameField);
		}

		// Token: 0x0600B657 RID: 46679 RVA: 0x002504B0 File Offset: 0x0024E6B0
		private static bool TrySetField(Exception e, string fieldName, string value, ref FieldInfo fieldInfo)
		{
			try
			{
				if (fieldInfo == null)
				{
					fieldInfo = typeof(Exception).GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
				}
				if (fieldInfo != null)
				{
					fieldInfo.SetValue(e, value);
					return true;
				}
			}
			catch (MemberAccessException)
			{
			}
			return false;
		}

		// Token: 0x04005D29 RID: 23849
		private static FieldInfo stackTraceField;

		// Token: 0x04005D2A RID: 23850
		private static FieldInfo classNameField;
	}
}

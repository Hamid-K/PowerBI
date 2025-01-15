using System;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace Microsoft.Cloud.Platform.EventsKit
{
	// Token: 0x0200036B RID: 875
	public static class ParameterMetadataFactory
	{
		// Token: 0x06001A0A RID: 6666 RVA: 0x0006046D File Offset: 0x0005E66D
		public static void AddParameterFactory(IParameterFactory parameterFactory)
		{
			ParameterMetadataFactory.SetConverters(parameterFactory);
		}

		// Token: 0x06001A0B RID: 6667 RVA: 0x00060478 File Offset: 0x0005E678
		private static IParameterFactory[] GetConverters()
		{
			IParameterFactory[] array = ParameterMetadataFactory.s_converters;
			IParameterFactory[] array2;
			lock (array)
			{
				array2 = ParameterMetadataFactory.s_converters;
			}
			return array2;
		}

		// Token: 0x06001A0C RID: 6668 RVA: 0x000604B8 File Offset: 0x0005E6B8
		private static void SetConverters(IParameterFactory parameterFactory)
		{
			IParameterFactory[] array = ParameterMetadataFactory.s_converters;
			lock (array)
			{
				IParameterFactory[] array2 = new IParameterFactory[ParameterMetadataFactory.s_converters.Length + 1];
				Array.Copy(ParameterMetadataFactory.s_converters, array2, ParameterMetadataFactory.s_converters.Length);
				array2[array2.Length - 1] = parameterFactory;
				ParameterMetadataFactory.s_converters = array2;
			}
		}

		// Token: 0x06001A0D RID: 6669 RVA: 0x00060520 File Offset: 0x0005E720
		private static string GetConstraintsErrorMessage(ParameterInfo paramInfo)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "Parameter '{1}' of type '{0}' in method '{2}' of '{3}' (assembly '{4}') has to conform to one of the following types: ", new object[]
			{
				paramInfo.ParameterType.Name,
				paramInfo.Name,
				paramInfo.Member.Name,
				paramInfo.Member.DeclaringType.FullName,
				paramInfo.Member.DeclaringType.Assembly.FullName
			});
			stringBuilder.Append(ParameterMetadataFactory.ValidTypesAsString);
			return stringBuilder.ToString();
		}

		// Token: 0x06001A0E RID: 6670 RVA: 0x000605AC File Offset: 0x0005E7AC
		public static ParameterMetadata Create(ParameterInfo methodParameter)
		{
			ParameterMetadata parameterMetadata = null;
			IParameterFactory[] converters = ParameterMetadataFactory.GetConverters();
			int num = 0;
			while (num < converters.Length && !converters[num].TryCreateParameterMetadata(methodParameter, out parameterMetadata))
			{
				num++;
			}
			if (parameterMetadata == null)
			{
				throw new InvalidOperationException(ParameterMetadataFactory.GetConstraintsErrorMessage(methodParameter));
			}
			return parameterMetadata;
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06001A0F RID: 6671 RVA: 0x000605EC File Offset: 0x0005E7EC
		private static string ValidTypesAsString
		{
			get
			{
				IParameterFactory[] converters = ParameterMetadataFactory.GetConverters();
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < converters.Length; i++)
				{
					IParameterFactory parameterFactory = converters[i];
					stringBuilder.Append(parameterFactory.ValidTypesAsString);
					if (i < converters.Length - 1)
					{
						stringBuilder.Append(", ");
					}
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x040008FE RID: 2302
		private static readonly IParameterFactory[] c_defaultConverters = new IParameterFactory[]
		{
			new KnownTypeParameterFactory(),
			new WireTypeParameterFactory(),
			new MonitoredErrorParameterFactory(),
			new PrivateInformationParameterFactory()
		};

		// Token: 0x040008FF RID: 2303
		private static IParameterFactory[] s_converters = ParameterMetadataFactory.c_defaultConverters;
	}
}

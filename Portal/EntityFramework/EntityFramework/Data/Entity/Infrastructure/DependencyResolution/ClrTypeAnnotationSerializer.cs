using System;
using System.IO;
using System.Reflection;

namespace System.Data.Entity.Infrastructure.DependencyResolution
{
	// Token: 0x020002A9 RID: 681
	internal class ClrTypeAnnotationSerializer : IMetadataAnnotationSerializer
	{
		// Token: 0x06002198 RID: 8600 RVA: 0x0005E4A4 File Offset: 0x0005C6A4
		public string Serialize(string name, object value)
		{
			return ((Type)value).AssemblyQualifiedName;
		}

		// Token: 0x06002199 RID: 8601 RVA: 0x0005E4B4 File Offset: 0x0005C6B4
		public object Deserialize(string name, string value)
		{
			try
			{
				return Type.GetType(value, false);
			}
			catch (FileLoadException)
			{
			}
			catch (TargetInvocationException)
			{
			}
			catch (BadImageFormatException)
			{
			}
			return null;
		}
	}
}

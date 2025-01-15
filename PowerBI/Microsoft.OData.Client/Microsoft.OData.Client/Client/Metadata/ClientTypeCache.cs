using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace Microsoft.OData.Client.Metadata
{
	// Token: 0x020000F5 RID: 245
	[DebuggerDisplay("{PropertyName}")]
	internal static class ClientTypeCache
	{
		// Token: 0x06000A6F RID: 2671 RVA: 0x00026EAC File Offset: 0x000250AC
		internal static Type ResolveFromName(string wireName, Type userType)
		{
			ClientTypeCache.TypeName typeName;
			typeName.Type = userType;
			typeName.Name = wireName;
			Dictionary<ClientTypeCache.TypeName, Type> dictionary = ClientTypeCache.namedTypes;
			Type type;
			bool flag2;
			lock (dictionary)
			{
				flag2 = ClientTypeCache.namedTypes.TryGetValue(typeName, out type);
			}
			if (!flag2)
			{
				string text = wireName;
				int num = wireName.LastIndexOf('.');
				if (0 <= num && num < wireName.Length - 1)
				{
					text = wireName.Substring(num + 1);
				}
				if (userType.Name == text)
				{
					type = userType;
				}
				else
				{
					foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
					{
						Type type2 = assembly.GetType(wireName, false);
						ClientTypeCache.ResolveSubclass(text, userType, type2, ref type);
						if (null == type2)
						{
							IEnumerable<Type> enumerable = null;
							try
							{
								enumerable = assembly.GetTypes();
							}
							catch (ReflectionTypeLoadException)
							{
							}
							if (enumerable != null)
							{
								foreach (Type type3 in enumerable)
								{
									ClientTypeCache.ResolveSubclass(text, userType, type3, ref type);
								}
							}
						}
					}
				}
				Dictionary<ClientTypeCache.TypeName, Type> dictionary2 = ClientTypeCache.namedTypes;
				lock (dictionary2)
				{
					ClientTypeCache.namedTypes[typeName] = type;
				}
			}
			return type;
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x00027034 File Offset: 0x00025234
		private static void ResolveSubclass(string wireClassName, Type userType, Type type, ref Type existing)
		{
			if (null != type && type.IsVisible() && wireClassName == type.Name && userType.IsAssignableFrom(type))
			{
				if (null != existing)
				{
					throw Error.InvalidOperation(Strings.ClientType_Ambiguous(wireClassName, userType));
				}
				existing = type;
			}
		}

		// Token: 0x04000604 RID: 1540
		private static readonly Dictionary<ClientTypeCache.TypeName, Type> namedTypes = new Dictionary<ClientTypeCache.TypeName, Type>(new ClientTypeCache.TypeNameEqualityComparer());

		// Token: 0x020001CD RID: 461
		private struct TypeName
		{
			// Token: 0x04000813 RID: 2067
			internal Type Type;

			// Token: 0x04000814 RID: 2068
			internal string Name;
		}

		// Token: 0x020001CE RID: 462
		private sealed class TypeNameEqualityComparer : IEqualityComparer<ClientTypeCache.TypeName>
		{
			// Token: 0x06000F24 RID: 3876 RVA: 0x0003277B File Offset: 0x0003097B
			public bool Equals(ClientTypeCache.TypeName x, ClientTypeCache.TypeName y)
			{
				return x.Type == y.Type && x.Name == y.Name;
			}

			// Token: 0x06000F25 RID: 3877 RVA: 0x000327A3 File Offset: 0x000309A3
			public int GetHashCode(ClientTypeCache.TypeName obj)
			{
				return obj.Type.GetHashCode() ^ obj.Name.GetHashCode();
			}
		}
	}
}

using System;
using System.Collections.Generic;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData
{
	// Token: 0x02000016 RID: 22
	public class ClrEnumMemberAnnotation
	{
		// Token: 0x060000A2 RID: 162 RVA: 0x00004074 File Offset: 0x00002274
		public ClrEnumMemberAnnotation(IDictionary<Enum, IEdmEnumMember> map)
		{
			if (map == null)
			{
				throw Error.ArgumentNull("map");
			}
			this._map = map;
			this._reverseMap = new Dictionary<IEdmEnumMember, Enum>();
			foreach (KeyValuePair<Enum, IEdmEnumMember> keyValuePair in map)
			{
				this._reverseMap.Add(keyValuePair.Value, keyValuePair.Key);
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000040F4 File Offset: 0x000022F4
		public IEdmEnumMember GetEdmEnumMember(Enum clrEnumMemberInfo)
		{
			IEdmEnumMember edmEnumMember;
			this._map.TryGetValue(clrEnumMemberInfo, out edmEnumMember);
			return edmEnumMember;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00004114 File Offset: 0x00002314
		public Enum GetClrEnumMember(IEdmEnumMember edmEnumMember)
		{
			Enum @enum;
			this._reverseMap.TryGetValue(edmEnumMember, out @enum);
			return @enum;
		}

		// Token: 0x0400001C RID: 28
		private IDictionary<Enum, IEdmEnumMember> _map;

		// Token: 0x0400001D RID: 29
		private IDictionary<IEdmEnumMember, Enum> _reverseMap;
	}
}

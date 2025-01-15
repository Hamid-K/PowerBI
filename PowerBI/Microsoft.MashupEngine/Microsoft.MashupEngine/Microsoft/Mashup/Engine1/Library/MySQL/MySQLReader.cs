using System;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.MySQL
{
	// Token: 0x02000919 RID: 2329
	internal class MySQLReader : ValueAdapterDbDataReader
	{
		// Token: 0x06004277 RID: 17015 RVA: 0x000E04DD File Offset: 0x000DE6DD
		public MySQLReader(DbDataReaderWithTableSchema reader, Type[] types, Func<object, object>[] adapters)
			: base(reader, types, adapters)
		{
		}

		// Token: 0x06004278 RID: 17016 RVA: 0x000E04E8 File Offset: 0x000DE6E8
		public override bool Read()
		{
			bool flag;
			try
			{
				flag = base.Read();
			}
			catch (FormatException ex)
			{
				throw ValueException.NewDataFormatError<Message0>(Strings.MySqlFormatException, Value.Null, ex);
			}
			return flag;
		}
	}
}

using System;
using System.Text;

namespace NLog
{
	// Token: 0x02000005 RID: 5
	public interface IJsonConverter
	{
		// Token: 0x0600001F RID: 31
		bool SerializeObject(object value, StringBuilder builder);
	}
}

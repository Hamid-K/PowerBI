using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Owin
{
	// Token: 0x0200000B RID: 11
	public interface IHeaderDictionary : IReadableStringCollection, IEnumerable<KeyValuePair<string, string[]>>, IEnumerable, IDictionary<string, string[]>, ICollection<KeyValuePair<string, string[]>>
	{
		// Token: 0x1700000F RID: 15
		string this[string key] { get; set; }

		// Token: 0x06000042 RID: 66
		IList<string> GetCommaSeparatedValues(string key);

		// Token: 0x06000043 RID: 67
		void Append(string key, string value);

		// Token: 0x06000044 RID: 68
		void AppendValues(string key, params string[] values);

		// Token: 0x06000045 RID: 69
		void AppendCommaSeparatedValues(string key, params string[] values);

		// Token: 0x06000046 RID: 70
		void Set(string key, string value);

		// Token: 0x06000047 RID: 71
		void SetValues(string key, params string[] values);

		// Token: 0x06000048 RID: 72
		void SetCommaSeparatedValues(string key, params string[] values);
	}
}

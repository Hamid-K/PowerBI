using System;
using System.Collections.Generic;

namespace Microsoft.ApplicationInsights.Extensibility
{
	// Token: 0x02000056 RID: 86
	public interface ISerializationWriter
	{
		// Token: 0x06000293 RID: 659
		void WriteProperty(string name, string value);

		// Token: 0x06000294 RID: 660
		void WriteProperty(string name, double? value);

		// Token: 0x06000295 RID: 661
		void WriteProperty(string name, int? value);

		// Token: 0x06000296 RID: 662
		void WriteProperty(string name, bool? value);

		// Token: 0x06000297 RID: 663
		void WriteProperty(string name, TimeSpan? value);

		// Token: 0x06000298 RID: 664
		void WriteProperty(string name, DateTimeOffset? value);

		// Token: 0x06000299 RID: 665
		void WriteProperty(string name, ISerializableWithWriter value);

		// Token: 0x0600029A RID: 666
		void WriteProperty(ISerializableWithWriter value);

		// Token: 0x0600029B RID: 667
		void WriteProperty(string name, IList<string> items);

		// Token: 0x0600029C RID: 668
		void WriteProperty(string name, IList<ISerializableWithWriter> items);

		// Token: 0x0600029D RID: 669
		void WriteProperty(string name, IDictionary<string, string> items);

		// Token: 0x0600029E RID: 670
		void WriteProperty(string name, IDictionary<string, double> items);

		// Token: 0x0600029F RID: 671
		void WriteStartObject(string name);

		// Token: 0x060002A0 RID: 672
		void WriteStartObject();

		// Token: 0x060002A1 RID: 673
		void WriteEndObject();
	}
}

using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x02000174 RID: 372
	internal interface IPropertyStore
	{
		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06000BC8 RID: 3016
		ReportObject Owner { get; }

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06000BC9 RID: 3017
		// (set) Token: 0x06000BCA RID: 3018
		IContainedObject Parent { get; set; }

		// Token: 0x06000BCB RID: 3019
		void RemoveProperty(int propertyIndex);

		// Token: 0x06000BCC RID: 3020
		object GetObject(int propertyIndex);

		// Token: 0x06000BCD RID: 3021
		T GetObject<T>(int propertyIndex);

		// Token: 0x06000BCE RID: 3022
		void SetObject(int propertyIndex, object value);

		// Token: 0x06000BCF RID: 3023
		void RemoveObject(int propertyIndex);

		// Token: 0x06000BD0 RID: 3024
		bool ContainsObject(int propertyIndex);

		// Token: 0x06000BD1 RID: 3025
		int GetInteger(int propertyIndex);

		// Token: 0x06000BD2 RID: 3026
		void SetInteger(int propertyIndex, int value);

		// Token: 0x06000BD3 RID: 3027
		void RemoveInteger(int propertyIndex);

		// Token: 0x06000BD4 RID: 3028
		bool ContainsInteger(int propertyIndex);

		// Token: 0x06000BD5 RID: 3029
		bool GetBoolean(int propertyIndex);

		// Token: 0x06000BD6 RID: 3030
		void SetBoolean(int propertyIndex, bool value);

		// Token: 0x06000BD7 RID: 3031
		void RemoveBoolean(int propertyIndex);

		// Token: 0x06000BD8 RID: 3032
		bool ContainsBoolean(int propertyIndex);

		// Token: 0x06000BD9 RID: 3033
		ReportSize GetSize(int propertyIndex);

		// Token: 0x06000BDA RID: 3034
		void SetSize(int propertyIndex, ReportSize value);

		// Token: 0x06000BDB RID: 3035
		void RemoveSize(int propertyIndex);

		// Token: 0x06000BDC RID: 3036
		bool ContainsSize(int propertyIndex);

		// Token: 0x06000BDD RID: 3037
		void IterateObjectEntries(VisitPropertyObject visitObject);
	}
}

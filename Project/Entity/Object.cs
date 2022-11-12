using System;
using System.Collections.Generic;

namespace Project.Entity;

public partial class Object
{
    public string Id { get; set; } = null!;

    public string? DisplayName { get; set; }

    public int IdUnit { get; set; }

    public int IdSuplier { get; set; }

    public string? Qrcode { get; set; }

    public string? BarCode { get; set; }

    public virtual Suplier IdSuplierNavigation { get; set; } = null!;

    public virtual Unit IdUnitNavigation { get; set; } = null!;

    public virtual ICollection<InputInfo> InputInfos { get; } = new List<InputInfo>();

    public virtual ICollection<OutputInfo> OutputInfos { get; } = new List<OutputInfo>();
}

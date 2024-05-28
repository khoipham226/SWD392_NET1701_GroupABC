﻿using System;
using System.Collections.Generic;

namespace ClassLibrary1.Models;

public partial class Token
{
    public int Id { get; set; }

    public string Value { get; set; } = null!;

    public int UserId { get; set; }

    public DateTime Expiration { get; set; }

    public virtual User User { get; set; } = null!;
}

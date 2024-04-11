using System;
using System.ComponentModel.DataAnnotations;

namespace Sample;

public class Tianqi
{
    [Key] [StringLength(10)] public string? Id { get; set; }

    public int High { get; set; }

    public int Low { get; set; }

    [StringLength(20)] // 最大长度为 20
    public string? Condition { get; set; }

    [StringLength(20)] // 最大长度为 20
    public string? Wind { get; set; }

    [StringLength(20)] // 最大长度为 20
    public string? Aqi { get; set; }

    public DateTime Date { get; set; }
}
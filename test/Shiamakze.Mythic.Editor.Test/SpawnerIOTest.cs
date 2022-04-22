using System.Diagnostics;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Shimakaze.Mythic.Editor.Models;
using Shimakaze.Mythic.Editor.Services;

namespace Shiamakze.Mythic.Editor.Test;

[TestClass]
public class SpawnerIOTest
{
    [TestMethod]
    public void TestRead()
    {
        string file = Path.GetFullPath(Path.Combine("Assets", "Spawner.yaml"));
        Assert.IsTrue(File.Exists(file), "找不到测试文件: \"{0}\"", file);
        using StreamReader reader = File.OpenText(file);
        var data = SpawnerIO.ReadFrom(reader);
        Assert.IsNotNull(data);
        Assert.AreEqual(string.Join(';', data.Keys), "SpawnerName;第二个Spawner;第三个Spawner");
    }
    [TestMethod]
    public void TestWrite()
    {
        StringWriter sw = new();
        Dictionary<string, Spawner> data = new()
        {
            { "Test1", new() { X = 123, Y = 321, Z = 1521 } }
        };
        SpawnerIO.WriteTo(data, sw);

        sw.Flush();
        Assert.AreEqual(sw.ToString().Trim(), @"Test1:
  MobName: ''
  World: ''
  SpawnerGroup: ''
  X: 123
  Y: 321
  Z: 1521
  Radius: 0
  RadiusY: 0
  UseTimer: false
  MaxMobs: 0
  MobLevel: 0
  MobsPerSpawn: 0
  Cooldown: 0
  CooldownTimer: 0
  Warmup: 0
  WarmupTimer: 0
  CheckForPlayers: false
  ActivationRange: 0
  LeashRange: 0
  HealOnLeash: false
  ResetThreatOnLeash: false
  ShowFlames: false
  Breakable: false
  Conditions: []
  ActiveMobs: 0");
    }
}
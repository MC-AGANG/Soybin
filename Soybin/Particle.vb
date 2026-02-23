Imports System.Windows.Media.Media3D
''' <summary>
''' 表示一个粒子
''' </summary>
Public MustInherit Class Particle
    ''' <summary>
    ''' 粒子起始坐标
    ''' </summary>
    Public Position As New Point3D
    ''' <summary>
    ''' 粒子的速度
    ''' </summary>
    Public Speed As New Vector3D
    ''' <summary>
    ''' 粒子的加速度
    ''' </summary>
    Public Acceleration As New Vector3D
    ''' <summary>
    ''' 是否使用相对坐标
    ''' </summary>
    Public Relative As Boolean = True
    ''' <summary>
    ''' 获取或设置粒子的标签
    ''' </summary>
    Public Tag As String
    ''' <summary>
    ''' 将粒子输出为Minecraft命令
    ''' </summary>
    ''' <param name="precision">坐标保留至小数点后几位</param>
    ''' <returns>粒子命令</returns>
    Public MustOverride Function Export(Optional precision As Integer = 4) As String
End Class

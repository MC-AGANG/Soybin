Imports System.Runtime.CompilerServices
Imports System.Windows.Media
''' <summary>
''' 包含一些公共的扩展方法
''' </summary>
Module Communal
    ''' <summary>
    ''' 获取列表中的元素从第几个开始重复
    ''' </summary>
    ''' <param name="e"></param>
    ''' <returns></returns>
    <Extension>
    Public Function GetReplication(e As List(Of Double)) As Integer
        If e.Count = 0 Then
            Return -1
        Else
            Dim i As Integer
            Dim lastvalue As Double = e(e.Count - 1)
            For i = e.Count - 1 To 0 Step -1
                If e(i) <> lastvalue Then
                    Exit For
                End If
            Next
            Return i + 1
        End If
    End Function
    ''' <summary>
    ''' 获取列表中的元素从第几个开始重复
    ''' </summary>
    ''' <param name="e"></param>
    ''' <returns></returns>
    <Extension>
    Public Function GetReplication(e As List(Of Color)) As Integer
        If e.Count = 0 Then
            Return -1
        Else
            Dim i As Integer
            Dim lastvalue As Color = e(e.Count - 1)
            For i = e.Count - 1 To 0 Step -1
                If e(i) <> lastvalue Then
                    Exit For
                End If
            Next
            Return i + 1
        End If
    End Function
    ''' <summary>
    ''' 获取列表中的元素从第几个开始重复
    ''' </summary>
    ''' <param name="e"></param>
    ''' <returns></returns>
    <Extension>
    Public Function GetReplication(e As List(Of Integer)) As Integer
        If e.Count = 0 Then
            Return -1
        Else
            Dim i As Integer
            Dim lastvalue As Integer = e(e.Count - 1)
            For i = e.Count - 1 To 0 Step -1
                If e(i) <> lastvalue Then
                    Exit For
                End If
            Next
            Return i + 1
        End If
    End Function
End Module

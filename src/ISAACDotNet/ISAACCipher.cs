using System;

public class ISAACCipher
{
    private const int Size = 256;
    private const int Mask = 1020;

    private int a;
    private int b;
    private int c;
    public int Count { get; private set; }

    private readonly int[] mem;
    private int[] rsl;

    public ISAACCipher()
    {
        mem = new int[Size];
        rsl = new int[Size];
        Init(false);
    }

    public ISAACCipher(int[] seed)
    {
        mem = new int[Size];
        rsl = new int[Size];
        Array.Copy(seed, 0, rsl, 0, seed.Length);
        Init(true);
    }

    public void Isaac()
    {
        c++;
        b += c;
        int i, j;

        for (i = 0, j = Size / 2; i < Size / 2;)
        {
            Calc(i++, j++);
            Calc(i++, j++);
            Calc(i++, j++);
            Calc(i++, j++);
        }

        for (j = 0; i < Size;)
        {
            Calc(i++, j++);
            Calc(i++, j++);
            Calc(i++, j++);
            Calc(i++, j++);
        }
    }

    private void Calc(int i, int j)
    {
        int x = mem[i];
        switch (i % 4)
        {
            case 0:
                a ^= a << 13;
                break;
            case 1:
                a ^= (int)((uint)a >> 6);
                break;
            case 2:
                a ^= a << 2;
                break;
            case 3:
                a ^= (int)((uint)a >> 16);
                break;
        }
        a += mem[j];
        mem[i] = x = mem[(x & Mask) >> 2] + a + b;
        rsl[i] = b = mem[((x >> 8) & Mask) >> 2] + x;
    }

    public void Init(bool flag)
    {
        int i;
        int a, b, c, d, e, f, g, h;
        a = b = c = d = e = f = g = h = -1640531527;

        for (i = 0; i < Size; i += 8)
        {
            if (flag)
            {
                a += rsl[i];
                b += rsl[i + 1];
                c += rsl[i + 2];
                d += rsl[i + 3];
                e += rsl[i + 4];
                f += rsl[i + 5];
                g += rsl[i + 6];
                h += rsl[i + 7];
            }
            Mix(a, b, c, d, e, f, g, h);
            mem[i] = a;
            mem[i + 1] = b;
            mem[i + 2] = c;
            mem[i + 3] = d;
            mem[i + 4] = e;
            mem[i + 5] = f;
            mem[i + 6] = g;
            mem[i + 7] = h;
        }

        if (flag)
            for (i = 0; i < Size; i += 8)
            {
                a += mem[i];
                b += mem[i + 1];
                c += mem[i + 2];
                d += mem[i + 3];
                e += mem[i + 4];
                f += mem[i + 5];
                g += mem[i + 6];
                h += mem[i + 7];
                Mix(a, b, c, d, e, f, g, h);
                mem[i] = a;
                mem[i + 1] = b;
                mem[i + 2] = c;
                mem[i + 3] = d;
                mem[i + 4] = e;
                mem[i + 5] = f;
                mem[i + 6] = g;
                mem[i + 7] = h;
            }

        Isaac();
        Count = Size;
    }

    private static void Mix(int a, int b, int c, int d, int e, int f, int g, int h)
    {
        a ^= b << 11; d += a; b += c;
        b ^= (int)((uint)c >> 2); e += b; c += d;
        c ^= d << 8; f += c; d += e;
        d ^= (int)((uint)e >> 16); g += d; e += f;
        e ^= f << 10; h += e; f += g;
        f ^= g << 4; a += f; g += h;
        g ^= h << 8; b += g; h += a;
        h ^= a >> 9; c += h; a += b;
    }

    public int Value()
    {
        if (Count == 0)
        {
            Isaac();
            Count = Size;
        }

        return rsl[--Count];
    }
}

import pdb

ci = [[4, 1, 4, 2, 3], [5, 1, 4, 2, -1, 6], [4, 19, 22, 24, 21], [4, 19, 22, 24, 25], [4, 2, -1, 0, 2]]
co = ["JOLLY", "NOT JOLLY", "NOT JOLLY", "JOLLY", "JOLLY"]

def main():
    o = []
    for i in ci:
        jl = set(range(1,i[0]))
        for a,b in zip(i[1:], i[2:]):
            jl.discard(abs(a - b))

        ot = "JOLLY" if len(jl) == 0 else "NOT JOLLY"
        o.append(ot)

    print(co == o)

if __name__ == "__main__":
    main()

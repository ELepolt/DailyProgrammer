#Spiral
import math
import numpy

# # Get the number
while(True):
    number = input('Please enter a positive integer: ')
    try:
        val = int(number)
        if val < 0:
            raise ValueError
        # 728 is the highest value that still appears nice on my screen
        if val > 728:
            print("Large values will appear poorly depending on screen size.")
            input('Press enter to continue.')
        break
    except ValueError:
        continue
number = int(number)

# Square root and ceiling to get the dimensions of the matrix
sqrt = math.sqrt(float(number))

height = math.ceil(sqrt)
# Get the median to find the middle of the array
# Adding a row if it's a perfect square.
if math.pow(height,2) == number:
    height += 1
if height % 2 == 0:
    height += 1

# Set height and width equal to each other to so that it's always a square
width = height

# Last number so that we fill an entire square
last_num = math.pow(height,2)
median = math.ceil(height / 2)

# Convert back to int for looping
# Subtract for indexing purposes
median = int(median-1)

# Initialize matrix
matrix = numpy.empty([height, width], dtype='int32')

# iteration will tell the loop how many numbers to enter into a row/column
iteration = 1

# direction will tell the position to increment or decrement. Start by going right from middle
# 1=r, 2=d, 3=l, 4=u
direction = 1

# Start in the middle
position = [median, median]

# Starting at the middle, an iteration occurs, then the direction changes, then the same iteration occurs
# So each iteration needs to happen twice, so print needs to be inside the iteration
i = 0
while True:
    for x in range(2):
        for j in range(iteration):
            if i <= number:
                matrix[position[0]][position[1]] = int(i)
            elif i > number:
                if i >= last_num:
                    break
                else:
                	# Fill spaces beyon 'number' with zeroes to fill out the square
                	# Zeroes will be removed in the final print
                    matrix[position[0]][position[1]] = 0
            i += 1
            if direction == 1:
                position[1] += 1
            elif direction == 2:
                position[0] += 1
            elif direction == 3:
                position[1] -= 1
            elif direction == 4:
                position[0] -= 1

            if j+1 == iteration:
                direction += 1
                if direction > 4:
                    direction = 1
    iteration += 1
    if i == last_num:
        break;

# Print the matrix all pretty like
# Doesn't print zeros and spaces numbers out.
for x in range(int(height)):
    row = ""
    for y in range(int(width)):
        if matrix[x][y] == 0:
            if x == median and y == median:
                row = row + str(matrix[x][y])
        else:
            row = row + str(matrix[x][y])
        row = row + "\t"
    print(row)